using System.Linq.Expressions;
using TranslationManagement.Domain.Entities;

namespace TranslationManagement.Interfaces.Dal.Repositories;

public interface ITranslationJobRepository
{
    Task<IEnumerable<TranslationJob>> GetAsync(Expression<Func<TranslationJob, bool>>? predicate = default, CancellationToken ct = default);

    Task CreateAsync(TranslationJob job, CancellationToken ct = default);

    Task UpdateAsync(TranslationJob job, CancellationToken ct = default);
}