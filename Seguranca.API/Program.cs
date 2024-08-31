using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NHibernate;
using NHibernate.Dialect;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(op =>
{
    op.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    op.JsonSerializerOptions.PropertyNamingPolicy = null;

    //op.JsonSerializerOptions.Converters.Add(new ConverterEnumEmNulo<ConvenioEnum>());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Seguranca.API", Version = "v1" });

    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header usando o esquema Bearer."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

//builder.Services.AddSingleton<ISessionFactory>(factory =>
//{
//    string connectionString = builder.Configuration.GetConnectionString("MySql");
//    return Fluently.Configure()
//    .Database((MySQLConfiguration.Standard
//    .Dialect<MySQL5Dialect>()
//    .ConnectionString(connectionString)
//    .FormatSql()
//    .ShowSql()))
//    .Mappings(x => x.FluentMappings.AddFromAssemblyOf<UsuarioMap>())
//    .BuildSessionFactory();
//});

builder.Services.AddScoped<NHibernate.ISession>(factory => factory.GetService<ISessionFactory>()!.OpenSession());
builder.Services.AddScoped<ITransaction>(factory => factory.GetService<NHibernate.ISession>()!.BeginTransaction());

//builder.Services.AddAutoMapper(typeof(EspecialidadesProfile));
//builder.Services.Scan(scan => scan
//    .FromAssemblyOf<UsuariosAppServico>()
//        .AddClasses()
//            .AsImplementedInterfaces()
//                .WithScopedLifetime());

//builder.Services.Scan(scan => scan
//    .FromAssemblyOf<UsuariosServico>()
//        .AddClasses()
//            .AsImplementedInterfaces()
//                .WithScopedLifetime());

//builder.Services.Scan(scan => scan
//    .FromAssemblyOf<UsuariosRepositorio>()
//        .AddClasses()
//            .AsImplementedInterfaces()
//                .WithScopedLifetime());

var app = builder.Build();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("../swagger/v1/swagger.json", "Seguranca.API");
        c.DisplayRequestDuration();
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
