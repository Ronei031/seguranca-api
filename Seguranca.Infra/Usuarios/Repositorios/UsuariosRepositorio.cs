using NHibernate;
using Seguranca.Dominio.Usuarios.Entidades;
using Seguranca.Dominio.Usuarios.Repositorios;
using Seguranca.Infra.Utils.Genericos;

namespace Seguranca.Infra.Usuarios.Repositorios
{
    public class UsuariosRepositorio : GenericoRepositorio<Usuario>, IUsuariosRepositorio
    {
        public UsuariosRepositorio(ISession session) : base(session)
        {
        }
    }
}
