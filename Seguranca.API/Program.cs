using Seguranca.API.Configuracoes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJsonConfiguration();
builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.AddAuthorizationPolicies();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddNHibernateConfiguration(builder.Configuration);
builder.Services.AddDependencyInjection();
builder.Services.AddCorsConfiguration();

var app = builder.Build();

app.UseCors();
app.UseApplicationMiddleware();
app.UseSwaggerConfiguration();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
