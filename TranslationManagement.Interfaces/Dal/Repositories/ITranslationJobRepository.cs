using TranslationManagement.Domain.Entities;

namespace TranslationManagement.Interfaces.Dal.Repositories;

public interface ITranslationJobRepository
{


    Task CreateAsync(TranslationJob job, CancellationToken ct = default);

    Task UpdateAsync(TranslationJob job, CancellationToken ct = default);
}