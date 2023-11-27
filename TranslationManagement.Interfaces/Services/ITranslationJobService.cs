using TranslationManagement.Domain.Entities;

namespace TranslationManagement.Interfaces.Services;

public interface ITranslationJobService
{
    Task<IEnumerable<TranslationJob>> GetAllAsync(CancellationToken ct = default);
    Task AddNewJobAsync(TranslationJob job, CancellationToken ct);
}