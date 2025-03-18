namespace Seguranca.API.Configuracoes
{
    public static class ApplicationMiddleware
    {
        public static void UseApplicationMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<AutorizacaoMiddlewareCustomizada>();
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Seguranca.API");
                c.DisplayRequestDuration();
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });
        }
    }
}