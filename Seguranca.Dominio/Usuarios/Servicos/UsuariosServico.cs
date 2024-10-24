using Seguranca.Dominio.Usuarios.Entidades;
using Seguranca.Dominio.Usuarios.Repositorios;
using Seguranca.Dominio.Usuarios.Servicos.Interfaces;
using Seguranca.Dominio.Util.Enumeradores;
using Seguranca.Dominio.Utils.Excecoes;

namespace Seguranca.Dominio.Usuarios.Servicos
{
    public class UsuariosServico : IUsuariosServico
    {
        private readonly IUsuariosRepositorio usuariosRepositorio;

        public UsuariosServico(IUsuariosRepositorio usuariosRepositorio)
        {
            this.usuariosRepositorio = usuariosRepositorio;
        }

        public async Task<Usuario> ValidarAsync(int id, CancellationToken cancellationToken = default)
        {
            Usuario usuario = await usuariosRepositorio.RecuperarAsync(id, cancellationToken);

            return usuario ?? throw new RegraDeNegocioExcecao($"Usuário com ID {id} não encontrado.");
        }

        public Usuario RecuperarPorNomeUsuario(string nomeUsuario)
        {
            IQueryable<Usuario> query = usuariosRepositorio.Query();

            Usuario usuario = query.FirstOrDefault(u => u.NomeUsuario == nomeUsuario);

            if (usuario == null)
            {
                throw new RegraDeNegocioExcecao($"Usuário '{nomeUsuario}' não encontrado.");
            }

            return usuario;
        }

        public void ValidarUsuarioOuEmailExistente(string nomeUsuario, string email)
        {
            IQueryable<Usuario> query = usuariosRepositorio.Query();

            if (query.Any(u => u.NomeUsuario == nomeUsuario))
            {
                throw new RegraDeNegocioExcecao($"Nome de usuário '{nomeUsuario}' já está em uso.");
            }

            if (query.Any(u => u.Email == email))
            {
                throw new RegraDeNegocioExcecao($"E-mail '{email}' já está em uso.");
            }
        }

        public string CriptografarSenha(string senha)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(senha);
        }

        public bool VerificarSenha(string senha, string senhaHash)
        {
            bool senhaValida =  BCrypt.Net.BCrypt.EnhancedVerify(senha, senhaHash);

            if (!senhaValida)
            {
                throw new RegraDeNegocioExcecao("Erro ao autenticar! Usuário ou senha inválidos.");
            }

            return senhaValida;
        }

        public void VerificarStatusUsuario(AtivoInativoEnum status)
        {
            if (status == AtivoInativoEnum.Inativo)
            {
                throw new RegraDeNegocioExcecao("Erro ao autenticar! Usuário Inativo.");
            }
        }
    }
}
