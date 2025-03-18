using Seguranca.Aplicacao.Usuarios.Profiles;
using Seguranca.Aplicacao.Usuarios.Servicos;
using Seguranca.Dominio.Permissoes.Servicos;
using Seguranca.Infra.Permissoes.Repositorios;

namespace Seguranca.API.Configuracoes
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UsuarioProfile));

            services.Scan(scan => scan
                .FromAssemblyOf<UsuariosAppServico>()
                    .AddClasses()
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            services.Scan(scan => scan
                .FromAssemblyOf<PermissoesServico>()
                    .AddClasses()
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            services.Scan(scan => scan
                .FromAssemblyOf<PermissoesRepositorio>()
                    .AddClasses()
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
        }
    }
}
