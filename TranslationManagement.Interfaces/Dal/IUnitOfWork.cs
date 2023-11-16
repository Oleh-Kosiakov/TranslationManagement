using TranslationManagement.Interfaces.Dal.Repositories;

namespace TranslationManagement.Interfaces.Dal;

/// <summary>
/// Can be used for transaction management later as well.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    ITranslationJobRepository TranslationJobRepository { get; }

    ITranslatorRepository TranslatorRepository { get; }

    Task SaveChangesAsync();
}