using NHibernate;
using Seguranca.Dominio.Permissoes.Entidades;
using Seguranca.Dominio.Permissoes.Repositorios;
using Seguranca.Infra.Utils.Genericos;

namespace Seguranca.Infra.Permissoes.Repositorios
{
    public class PermissoesRepositorio : GenericoRepositorio<Permissao>, IPermissoesRepositorio
    {
        public PermissoesRepositorio(ISession session) : base(session)
        {
        }
    }
}
