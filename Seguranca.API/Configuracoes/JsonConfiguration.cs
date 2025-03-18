using System.Text.Json.Serialization;

namespace Seguranca.API.Configuracoes
{
    public static class JsonConfiguration
    {
        public static void AddJsonConfiguration(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(op =>
            {
                op.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                op.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
        }
    }
}
