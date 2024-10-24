using FluentNHibernate.Mapping;
using Seguranca.Dominio.Permissoes.Entidades;

namespace Seguranca.Infra.Permissoes.Mapeamentos
{
    public class PermissaoMap : ClassMap<Permissao>
    {
        public PermissaoMap()
        {
            Table("permissao");

            Id(x => x.Id);
            Map(x => x.Nome).Not.Nullable().Length(100);
            Map(x => x.Descricao).Length(250);

            HasMany(x => x.RolePermissoes)
                .Cascade.All()
                .Inverse()
                .KeyColumn("IdPermissao");
        }
    }
}
