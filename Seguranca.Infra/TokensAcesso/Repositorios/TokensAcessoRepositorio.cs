using NHibernate;
using Seguranca.Dominio.TokensAcesso.Entidades;
using Seguranca.Dominio.TokensAcesso.Repositorios;
using Seguranca.Infra.Utils.Genericos;

namespace Seguranca.Infra.TokensAcesso.Repositorios
{
    public class TokensAcessoRepositorio : GenericoRepositorio<TokenAcesso>, ITokenAcessoRepositorio
    {
        public TokensAcessoRepositorio(ISession session) : base(session)
        {
        }
    }
}