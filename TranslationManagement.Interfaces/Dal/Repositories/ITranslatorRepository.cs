using System.Linq.Expressions;
using TranslationManagement.Domain.Entities;

namespace TranslationManagement.Interfaces.Dal.Repositories;

public interface ITranslatorRepository
{
    Task<IEnumerable<Translator>> GetAsync(Expression<Func<Translator, bool>>? predicate = default, CancellationToken ct = default);
    Task CreateAsync(Translator entity, CancellationToken ct = default);
    Task UpdateAsync(Translator entity, CancellationToken ct = default);
}