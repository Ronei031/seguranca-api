using NHibernate;
using Seguranca.Dominio.UsuariosRoles.Entidades;
using Seguranca.Dominio.UsuariosRoles.Repositorios;
using Seguranca.Infra.Utils.Genericos;

namespace Seguranca.Infra.UsuariosRoles.Repositorios
{
    public class UsuariosRolesRepositorio : GenericoRepositorio<UsuarioRole>, IUsuariosRolesRepositorio
    {
        public UsuariosRolesRepositorio(ISession session) : base(session)
        {
        }
    }
}
