using FluentNHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using Seguranca.Infra.Usuarios.Mapeamentos;

namespace Seguranca.API.Configuracoes
{
    public static class NHibernateConfiguration
    {
        public static void AddNHibernateConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ISessionFactory>(factory =>
            {
                string connectionString = configuration.GetConnectionString("MySql") ?? "";
                return Fluently.Configure()
                    .Database(MySQLConfiguration.Standard
                        .Dialect<MySQL5Dialect>()
                        .ConnectionString(connectionString)
                        .FormatSql()
                        .ShowSql())
                    .Mappings(x => x.FluentMappings.AddFromAssemblyOf<UsuarioMap>())
                    .BuildSessionFactory();
            });

            services.AddScoped(factory => factory.GetService<ISessionFactory>()!.OpenSession());
            services.AddScoped(factory => factory.GetService<NHibernate.ISession>()!.BeginTransaction());
        }
    }
}
