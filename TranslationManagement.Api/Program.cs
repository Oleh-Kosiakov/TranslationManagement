using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TranslationManagement.Bll;
using TranslationManagement.Dal;
using TranslationManagement.Dal.EF;

namespace TranslationManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder);

            var app = builder.Build();
            ApplyDbMigrations(app);


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            ConfigureApplication(app);
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTranslatorsDal();
            builder.Services.AddTranslatorsBll();

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DalMapperProfile>();
                cfg.AddProfile<ApiMapperProfile>();
            });
        }

        private static void ApplyDbMigrations(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                try
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<TranslationsDbContext>();
                    dbContext.Database.Migrate();
                    logger.LogInformation("Migrations applied successfully");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while applying migrations");
                }
            }
        }

        private static void ConfigureApplication(WebApplication app)
        {
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}