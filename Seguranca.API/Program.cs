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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseApplicationMiddleware();
app.UseSwaggerConfiguration();
//app.UseHttpsRedirection();
app.MapControllers();
app.Run();
