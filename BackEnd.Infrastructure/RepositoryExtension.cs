
using BackEnd.Core.Repository;
using BackEnd.Infrastructure.DataBase;
using BackEnd.Infrastructure.Repository;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BackEnd.Infrastructure;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {

       
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IItemTrabajoRepository, ItemTrabajoRepository>();
        
        return services;
    }
    public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration config)
    {
        var backendConnection = config.GetConnectionString("backendConnection");

        services.AddDbContext<BackEndContext>(options =>
            options.UseSqlServer(backendConnection)); // Usa UseSqlServer si estás trabajando con SQL Server

        return services;
    }
    public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration config)
    {
        var key = config["Jwt:Key"];
        var issuer = config["Jwt:Issuer"];
        var audience = config["Jwt:Audience"];

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),

                ValidateIssuer = true,
                ValidIssuer = issuer,

                ValidateAudience = true,
                ValidAudience = audience,

                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(15) // tolerancia
            };
        });

        return services;
    }

}
