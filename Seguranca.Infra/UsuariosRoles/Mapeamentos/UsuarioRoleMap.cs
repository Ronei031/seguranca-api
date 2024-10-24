using FluentNHibernate.Mapping;
using Seguranca.Dominio.UsuariosRoles.Entidades;

namespace Seguranca.Infra.UsuariosRoles.Mapeamentos
{
    public class UsuarioRoleMap : ClassMap<UsuarioRole>
    {
        public UsuarioRoleMap()
        {
            Table("usuariorole");

            CompositeId()
                .KeyReference(x => x.Usuario, "IdUsuario")
                .KeyReference(x => x.Role, "IdRole");
        }
    }
}
