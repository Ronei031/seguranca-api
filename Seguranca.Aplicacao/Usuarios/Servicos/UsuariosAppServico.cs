using AutoMapper;
using Microsoft.Extensions.Configuration;
using Seguranca.Aplicacao.Usuarios.Servicos.Interfaces;
using Seguranca.Aplicacao.Utils.Transacoes.Interface;
using Seguranca.DataTransfer.Usuarios.Requests;
using Seguranca.DataTransfer.Usuarios.Responses;
using Seguranca.Dominio.RabbitMq.Repositorios;
using Seguranca.Dominio.Roles.Entidades;
using Seguranca.Dominio.Roles.Enumeradores;
using Seguranca.Dominio.Roles.Servicos.Interfaces;
using Seguranca.Dominio.TokensAcesso.Servicos.Interfaces;
using Seguranca.Dominio.Usuarios.Entidades;
using Seguranca.Dominio.Usuarios.Repositorios;
using Seguranca.Dominio.Usuarios.Servicos.Interfaces;
using Seguranca.Dominio.UsuariosRoles.Entidades;
using Seguranca.Dominio.UsuariosRoles.Servicos.Interfaces;
using Seguranca.Dominio.Utils.Enumeradores;
using System.Text;

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
        private readonly IConfiguration configuration;
        private readonly IRabbitMqRepositorio rabbitMqRepositorio;

        public UsuariosAppServico(IMapper mapper, IUnitOfWork unitOfWork, IUsuariosRepositorio usuariosRepositorio, IUsuariosServico usuariosServico, ITokensAcessoServico tokensAcessoServico, IRolesServico rolesServico, IUsuarioRolesServico usuarioRolesServico, IConfiguration configuration, IRabbitMqRepositorio rabbitMqRepositorio)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.usuariosRepositorio = usuariosRepositorio;
            this.usuariosServico = usuariosServico;
            this.tokensAcessoServico = tokensAcessoServico;
            this.rolesServico = rolesServico;
            this.usuarioRolesServico = usuarioRolesServico;
            this.configuration = configuration;
            this.rabbitMqRepositorio = rabbitMqRepositorio;
        }

        public async Task<UsuarioResponse> CriarUsuarioAsync(UsuarioRequest usuarioRequest, CancellationToken cancellationToken = default)
        {
            usuariosServico.ValidarUsuarioOuEmailExistente(usuarioRequest.NomeUsuario, usuarioRequest.Email);

            try
            {
                unitOfWork.BeginTransaction();

                string senhaHashCriptografado = usuariosServico.CriptografarSenha(usuarioRequest.Senha);

                Usuario usuario = new(usuarioRequest.NomeCompleto, usuarioRequest.NomeUsuario, usuarioRequest.Email, senhaHashCriptografado, usuarioRequest.UrlImagemPerfil);

                Role role = rolesServico.RecuperarPorNome(RoleEnum.Usuario.GetDescription());

                Usuario usuarioResponse = await usuariosRepositorio.InserirAsync(usuario, cancellationToken);

                UsuarioRole usuarioRole = await usuarioRolesServico.InserirAsync(usuarioResponse.Id, usuarioResponse, role.Id, role, cancellationToken);

                usuario.UsuarioRoles.Add(usuarioRole);

                //JsonSerializerOptions jsonSerializerOptions = new()
                //{
                //    ReferenceHandler = ReferenceHandler.Preserve
                //};

                //JsonSerializerOptions options = jsonSerializerOptions;

                //// Serializar o objeto com o novo comportamento
                //string usuarioSerealizado = JsonSerializer.Serialize(usuarioResponse, options);

                //string mensagem = $"Usuário Cadastrado {usuarioResponse}";

                //await rabbitMqRepositorio.EnviarMensagemAsync(mensagem, "UsuarioCadastro", cancellationToken);

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

                bool loginValido = usuariosServico.VerificarSenha(request.Senha, usuario.SenhaHash);

                usuariosServico.VerificarStatusUsuario(usuario.Status);

                await AtualizarDataUltimoLogin(usuario, loginValido, cancellationToken);

                string chave = configuration["Jwt:Key"] ?? "";

                byte[] key = Encoding.ASCII.GetBytes(chave);

                string token = await tokensAcessoServico.GerarTokenAsync(usuario, key, cancellationToken);

                //JsonSerializerOptions jsonSerializerOptions = new()
                //{
                //    ReferenceHandler = ReferenceHandler.Preserve
                //};

                //string usuarioSerealizado = JsonSerializer.Serialize(usuario, jsonSerializerOptions);

                //string mensagem = $"Usuário logado: {usuarioSerealizado}";

                //await rabbitMqRepositorio.EnviarMensagemAsync(mensagem, "UsuarioLogin", cancellationToken);

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
