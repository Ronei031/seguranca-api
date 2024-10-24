using FluentNHibernate.Mapping;
using NHibernate.Type;
using Seguranca.Dominio.TokensAcesso.Entidades;
using Seguranca.Dominio.Util.Enumeradores;

namespace Seguranca.Infra.TokensAcesso.Mapeamentos
{
    public class TokenAcessoMap : ClassMap<TokenAcesso>
    {
        public TokenAcessoMap()
        {
            Table("tokenacesso");

            Id(x => x.Id);
            Map(x => x.Token).Not.Nullable();
            Map(x => x.DataExpiracao).Not.Nullable();
            Map(x => x.Revogado).Not.Nullable().CustomType<EnumType<AtivoInativoEnum>>();
            Map(x => x.DataCriacao).Not.Nullable();

            References(x => x.Usuario)
                .Column("IdUsuario")
                .Not.Nullable();
        }
    }
}
