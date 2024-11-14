using CB.Application.Extentions;
using CB.Domain;
using CB.Infrastructure;

namespace CB.Application {

    public class Program {

        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCBContext(builder.Configuration);
            builder.Services.AddAuth(builder.Configuration);

            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwag();
            builder.Services.AddCors();

            builder.Services.AddMiddlewares();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddMediatR();
            builder.Services.AddRedis(builder.Configuration);
            builder.Services.AddResources();
            builder.Services.AddJobs(builder.Configuration);

            // Infastructure
            builder.Services.AddInternalService();

            var app = builder.Build();

            app.UseSwag();
            app.UseHttpsRedirection();
            app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().WithExposedHeaders("*"));
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseMiddlewares();
            app.MapControllers();

            app.Services.AutoMigration();
            app.Services.UseJobs();

            app.Run();
        }
    }
}
