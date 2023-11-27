using External.ThirdParty.Services;
using Microsoft.Extensions.DependencyInjection;
using TranslationManagement.Bll.Services;
using TranslationManagement.Interfaces.Services;

namespace TranslationManagement.Bll;

public static class BllDIModule
{
    public static void AddTranslatorsBll(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ITranslatorService, TranslatorService>();
        serviceCollection.AddScoped<ITranslationJobService, TranslationJobService>();
        serviceCollection.AddScoped<INotificationService, UnreliableNotificationService>();
    }
}