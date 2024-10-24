using FluentNHibernate.Mapping;
using Seguranca.Dominio.RolesPermissoes.Entidades;

namespace Seguranca.Infra.RolesPermissoes.Mapeamentos
{
    public class RolePermissaoMap : ClassMap<RolePermissao>
    {
        public RolePermissaoMap()
        {
            Table("rolepermissao");

            CompositeId()
                .KeyReference(x => x.Role, "IdRole")
                .KeyReference(x => x.Permissao, "IdPermissao");
        }
    }
}
