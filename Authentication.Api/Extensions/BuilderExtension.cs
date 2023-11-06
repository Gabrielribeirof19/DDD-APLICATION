using authentication.domain.entities;
using authentication.domain.Entities;
using authentication.domain.Handlers;
using authentication.domain.Infra.Contexts;
using authentication.domain.Infra.Repositories;
using authentication.Domain.Configuration;
using authentication.Domain.Handlers;
using authentication.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Api.Extensions
{

    public static class BuilderExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {

            Configuration.SecretsConfiguration.ApiKey =
                builder.Configuration.GetSection("Secrets").GetValue<string>("ApiKey") ?? string.Empty;

            Configuration.SecretsConfiguration.JwtPrivateKey =
                builder.Configuration.GetSection("Secrets").GetValue<string>("JwtPrivateKey") ?? string.Empty;

            Configuration.SecretsConfiguration.PasswordSaltKey =
                builder.Configuration.GetSection("Secrets").GetValue<string>("PasswordSaltKey") ?? string.Empty;

            Configuration.DatabaseConfiguration.ConnectionString =
                builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnectionString") ?? string.Empty;
        }

        public static void AddDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.DatabaseConfiguration.ConnectionString, b =>
                    b.MigrationsAssembly("Authentication.Api")));
        }

        public static void AddJwtAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(Configuration.SecretsConfiguration.JwtPrivateKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            builder.Services.AddAuthorization();
        }
        public static void AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPersonRepository, PersonRepository<Person>>();
        }
        public static void AddHandlers(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<AuthenticateHandler>();
            builder.Services.AddTransient<PersonHandler>();
        }
    }

}