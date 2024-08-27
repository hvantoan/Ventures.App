using System.Text;
using CB.Api.Extentions;
using CB.Application;
using CB.Domain;
using CB.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CB.Api {

    public class Program {

        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCBContext(builder.Configuration);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecret"]!))
                });

            //builder.Services.AddAuthorizationBuilder().SetDefaultPolicy(new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build());

            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwag();
            builder.Services.AddJWT(builder.Configuration);
            builder.Services.AddCors();

            builder.Services.AddMiddlewares();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddMediatR();
            builder.Services.AddRedis(builder.Configuration);

            var app = builder.Build();

            app.UseSwag();
            app.UseHttpsRedirection();
            app.UseCors();
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseMiddlewares();
            app.MapControllers();
            app.Services.AutoMigration();
            app.Run();
        }
    }
}
