using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TranslationManagement.Dal.EF;
using TranslationManagement.Dal.Repositories;
using TranslationManagement.Interfaces.Dal;
using TranslationManagement.Interfaces.Dal.Repositories;

namespace TranslationManagement.Dal;

public static class DalDIModule
{
    public static void AddTranslatorsDal(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<TranslationsDbContext>(options =>
            options.UseSqlite("Data Source=TranslationAppDatabase.db"));

        serviceCollection.AddScoped<ITranslationJobRepository, TranslationJobRepository>();
        serviceCollection.AddScoped<ITranslatorRepository, TranslatorRepository>();
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}