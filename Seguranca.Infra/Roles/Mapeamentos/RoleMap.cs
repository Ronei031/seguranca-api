using FluentNHibernate.Mapping;
using NHibernate.Type;
using Seguranca.Dominio.Roles.Entidades;
using Seguranca.Dominio.Roles.Enumeradores;

namespace Seguranca.Infra.Roles.Mapeamentos
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Table("role");

            Id(x => x.Id);
            Map(x => x.Nome).Not.Nullable();
            Map(x => x.Descricao).Length(250);

            HasMany(x => x.UsuarioRoles)
                .Cascade.All()
                .Inverse()
                .KeyColumn("IdRole");

            HasMany(x => x.RolePermissoes)
                .Cascade.All()
                .Inverse()
                .KeyColumn("IdRole");
        }
    }
}
