using TranslationManagement.Domain.Entities;
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Interfaces.Services;

public interface ITranslatorService
{
    Task<IEnumerable<Translator>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Translator>> GetTranslatorsByNameAsync(string name, CancellationToken ct = default);
    Task AddTranslatorAsync(Translator translator, CancellationToken ct = default);

    Task UpdateTranslatorStatusAsync(
        int translatorId,
        TranslatorStatus translatorStatus,
        CancellationToken ct = default);
}