using Seguranca.Dominio.Usuarios.Entidades;
using Seguranca.Dominio.Utils.Enumeradores;
using Seguranca.Dominio.Utils.Excecoes;

namespace Seguranca.Dominio.TokensAcesso.Entidades
{
    public class TokenAcesso
    {
        public virtual int Id { get; protected set; }
        public virtual string Token { get; protected set; }
        public virtual int IdUsuario { get; protected set; }
        public virtual Usuario Usuario { get; protected set; }
        public virtual DateTime DataExpiracao { get; protected set; }
        public virtual SimNaoEnum Revogado { get; protected set; }
        public virtual DateTime DataCriacao { get; protected set; }

        protected TokenAcesso()
        {
        }

        public TokenAcesso(string token, int idUsuario, Usuario usuario, SimNaoEnum revogado, DateTime dataExpiracao)
        {
            SetToken(token);
            SetIdUsuario(idUsuario);
            SetUsuario(usuario);
            SetDataExpiracao(dataExpiracao);
            SetRevogado(revogado);
            SetDataCriacao(DateTime.Now);
        }

        public virtual void SetToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new AtributoObrigatorioExcecao("Token");
            }

            Token = token;
        }

        public virtual void SetIdUsuario(int idUsuario)
        {
            if (idUsuario <= 0)
            {
                throw new AtributoObrigatorioExcecao("IdUsuario");
            }

            IdUsuario = idUsuario;
        }

        public virtual void SetUsuario(Usuario usuario)
        {
            Usuario = usuario;
        }

        public virtual void SetDataExpiracao(DateTime dataExpiracao)
        {
            DataExpiracao = dataExpiracao;
        }

        public virtual void SetRevogado(SimNaoEnum revogado)
        {
            Revogado = revogado;
        }

        public virtual void SetDataCriacao(DateTime dataCriacao)
        {
            DataCriacao = dataCriacao;
        }
    }
}
