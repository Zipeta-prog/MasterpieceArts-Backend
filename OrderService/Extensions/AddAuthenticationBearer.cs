using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OrderService.Extensions
{
    public static class AddAuthenticationBearer
    {
        public static WebApplicationBuilder AddAuth(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    //things that should be validated
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,


                    ValidAudience = builder.Configuration.GetSection("JwtOptions:Audience").Value,
                    ValidIssuer = builder.Configuration.GetSection("JwtOptions:Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtOptions:SecretKey").Value))
                };
            });

            return builder;
        }
    }
}
