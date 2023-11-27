using TranslationManagement.Interfaces.Dal.Repositories;

namespace TranslationManagement.Interfaces.Dal;

/// <summary>
/// Can be used for transaction management later as well.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    ITranslationJobRepository TranslationJobRepository { get; }

    ITranslatorRepository TranslatorRepository { get; }

    /// <summary>
    /// Saves the stashed changes in the DB
    /// </summary>
    /// <returns>Returns the number of the affected entities</returns>
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}