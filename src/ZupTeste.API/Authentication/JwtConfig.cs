using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ZupTeste.Infra.Settings;

namespace ZupTeste.API.Authentication
{
    public static class JwtConfig
    {
        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, AppSettings appSettings)
        {
            // JWT
            var key = Encoding.ASCII.GetBytes(appSettings.JwtSettings.Secret);

            services
                .AddAuthorization()
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = appSettings.JwtSettings.Audience,
                        ValidIssuer = appSettings.JwtSettings.Issuer,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddSingleton<JwtHelper>();

            return services;
        }

        public static IApplicationBuilder UseJwt(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}