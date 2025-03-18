namespace Seguranca.API.Configuracoes
{
    public static class AuthorizationPolicies
    {
        public static void AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy("Admin", policy => policy.RequireRole("Admin"))
                .AddPolicy("Usuario", policy => policy.RequireRole("Usuario"))
                .AddPolicy("Visitante", policy => policy.RequireRole("Visitante"))
                .AddPolicy("Desenvolvedor", policy => policy.RequireRole("Desenvolvedor"));
        }
    }
}
