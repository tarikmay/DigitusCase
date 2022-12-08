using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System.Text;
using System.Text.Json;
using UserLoginApp.Business.Concrete;
using UserLoginApp.Business.Helper.JwtToken;
using UserLoginApp.Business.Helper.Mailler;
using UserLoginApp.Business.Helper.Mailler.Model;
using UserLoginApp.Business.Interfaces;
using UserLoginApp.DataAccess.Conrete.Mongo;
using UserLoginApp.DataAccess.Conrete.Mongo.Model;
using UserLoginApp.DataAccess.Interfaces;

namespace UserLoginApp.Business.IOC
{
    public static class CustomIOC
    {
        public static void AddCustomDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //MongoDbSettings
            services.Configure<MongoSettingsModel>(c => configuration.GetSection("MongoDbConn"));

            //Repos
            services.AddScoped(typeof(IGenericRepositoryMD<>), typeof(GenericRepositoryMD<>));

            //Services
            services.AddSingleton<IEMailService, EMailManager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<ITokenService, TokenManager>();
            services.AddScoped<IRequestTimeService, RequestTimeManager>();

            

            //JWT 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = true,
                  ValidateAudience = true,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = configuration["Jwt:Issuer"],
                  ValidAudience = configuration["Jwt:Issuer"],
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
              };
              options.Events = new JwtBearerEvents()
              {
                  OnChallenge = context =>
                  {
                      context.HandleResponse();
                      context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                      context.Response.ContentType = "application/json";
                      if (string.IsNullOrEmpty(context.Error))
                          context.Error = "İstek Reddedildi!";
                      if (string.IsNullOrEmpty(context.ErrorDescription))
                          context.ErrorDescription = "Token Bulunamadı.";

                      if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
                      {
                          var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                          context.Response.Headers.Add("x-token-expired", authenticationException.Expires.ToString("o"));
                          context.ErrorDescription = $"Token zaman aşımı {authenticationException.Expires.ToString("o")}";
                      }

                      return context.Response.WriteAsync(JsonSerializer.Serialize(new
                      {
                          error = "İstek Reddedildi!",
                          error_description = "Token Geçersiz."
                      }));
                  }


              };
          });

            //Swagger 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "UserLoginApp_API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Header a Token Eklemek Gerekiyor. \r\n\r\n 'Bearer' yazıp bir boşluk bırakıp Login kısmından aldığımız Bearer Token ı yazıyoruz.\r\n\r\nÖrn: \"Bearer aSDFAasfa\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
            });

            //Email
            var emailConfig = configuration.GetSection("EmailConn").Get<EMailSettings>();
            services.AddSingleton(emailConfig);

            //Cache
            services.AddMemoryCache();

            //SignalR
            services.AddSignalR();


        }
    }
}
