using AutoMapper;
using NHibernate.Engine.Query;
using Seguranca.Aplicacao.Usuarios.Servicos.Interfaces;
using Seguranca.Aplicacao.Utils.Transacoes.Interface;
using Seguranca.DataTransfer.Usuarios.Requests;
using Seguranca.DataTransfer.Usuarios.Responses;
using Seguranca.Dominio.Roles.Entidades;
using Seguranca.Dominio.Roles.Enumeradores;
using Seguranca.Dominio.Roles.Servicos.Interfaces;
using Seguranca.Dominio.TokensAcesso.Servicos.Interfaces;
using Seguranca.Dominio.Usuarios.Entidades;
using Seguranca.Dominio.Usuarios.Repositorios;
using Seguranca.Dominio.Usuarios.Servicos.Interfaces;
using Seguranca.Dominio.UsuariosRoles.Entidades;
using Seguranca.Dominio.UsuariosRoles.Servicos.Interfaces;

namespace Seguranca.Aplicacao.Usuarios.Servicos
{
    public class UsuariosAppServico : IUsuariosAppServico
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUsuariosRepositorio usuariosRepositorio;
        private readonly IUsuarioRolesServico usuarioRolesServico;
        private readonly IUsuariosServico usuariosServico;
        private readonly ITokensAcessoServico tokensAcessoServico;
        private readonly IRolesServico rolesServico;

        public UsuariosAppServico(IMapper mapper, IUnitOfWork unitOfWork, IUsuariosRepositorio usuariosRepositorio, IUsuariosServico usuariosServico, ITokensAcessoServico tokensAcessoServico, IRolesServico rolesServico, IUsuarioRolesServico usuarioRolesServico)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.usuariosRepositorio = usuariosRepositorio;
            this.usuariosServico = usuariosServico;
            this.tokensAcessoServico = tokensAcessoServico;
            this.rolesServico = rolesServico;
            this.usuarioRolesServico = usuarioRolesServico;
        }

        public async Task<UsuarioResponse> CriarUsuarioAsync(UsuarioRequest usuarioRequest, CancellationToken cancellationToken = default)
        {
            usuariosServico.ValidarUsuarioOuEmailExistente(usuarioRequest.NomeUsuario, usuarioRequest.Email);

            try
            {
                unitOfWork.BeginTransaction();

                string senhaHashCriptografado = usuariosServico.CriptografarSenha(usuarioRequest.Senha);

                Usuario usuario = new(usuarioRequest.NomeCompleto, usuarioRequest.NomeUsuario, usuarioRequest.Email, senhaHashCriptografado);

                Role role = rolesServico.RecuperarPorNome(RoleEnum.Usuario);

                Usuario usuarioResponse = await usuariosRepositorio.InserirAsync(usuario, cancellationToken);

                UsuarioRole usuarioRole = await usuarioRolesServico.InserirAsync(usuarioResponse.Id, usuarioResponse, role.Id, role, cancellationToken);

                usuario.UsuarioRoles.Add(usuarioRole);

                unitOfWork.Commit();

                return mapper.Map<UsuarioResponse>(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UsuarioLoginResponse> EfetuarLoginAsync(UsuarioLoginRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                unitOfWork.BeginTransaction();

                Usuario usuario = usuariosServico.RecuperarPorNomeUsuario(request.NomeUsuario);

                usuariosServico.VerificarStatusUsuario(usuario.Status);

                bool loginValido = usuariosServico.VerificarSenha(request.Senha, usuario.SenhaHash);

                await AtualizarDataUltimoLogin(usuario, loginValido, cancellationToken);

                string token = await tokensAcessoServico.GerarTokenAsync(usuario, cancellationToken);

                unitOfWork.Commit();

                return new UsuarioLoginResponse
                {
                    Token = token,
                    Id = usuario.Id,
                };
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                throw new ArgumentException(ex.Message);
            }
        }

        #region Métodos Privados
        private async Task AtualizarDataUltimoLogin(Usuario usuario, bool loginValido, CancellationToken cancellationToken)
        {
            if (loginValido)
            {
                usuario.SetUltimoLogin(DateTime.Now);

                await usuariosRepositorio.EditarAsync(usuario, cancellationToken);
            }
        }
        #endregion
    }
}
