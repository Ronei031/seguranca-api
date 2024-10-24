using NHibernate;
using Seguranca.Dominio.Roles.Entidades;
using Seguranca.Dominio.Roles.Repositorios;
using Seguranca.Infra.Utils.Genericos;

namespace Seguranca.Infra.Roles.Repositorios
{
    public class RolesRepositorio : GenericoRepositorio<Role>, IRolesRepositorio
    {
        public RolesRepositorio(ISession session) : base(session)
        {
        }
    }
}
