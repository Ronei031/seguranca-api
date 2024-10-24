using FluentNHibernate.Mapping;
using NHibernate.Type;
using Seguranca.Dominio.Usuarios.Entidades;
using Seguranca.Dominio.Util.Enumeradores;

namespace Seguranca.Infra.Usuarios.Mapeamentos
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Table("usuario");

            Id(x => x.Id);
            Map(x => x.NomeCompleto).Not.Nullable().Length(100);
            Map(x => x.NomeUsuario).Not.Nullable().Length(100);
            Map(x => x.Email).Not.Nullable().Length(100);
            Map(x => x.SenhaHash).Not.Nullable();
            Map(x => x.Status).Not.Nullable().CustomType<EnumType<AtivoInativoEnum>>();
            Map(x => x.DataCriacao).Not.Nullable();
            Map(x => x.UltimoLogin).Nullable();

            HasMany(x => x.UsuarioRoles)
                .Cascade.All()
                .Inverse()
                .KeyColumn("IdUsuario");

            HasMany(x => x.TokensAcesso)
                .Cascade.All()
                .Inverse()
                .KeyColumn("IdUsuario");
        }
    }
}
