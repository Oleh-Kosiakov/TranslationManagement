using TranslationManagement.Interfaces.Dal.Repositories;

namespace TranslationManagement.Interfaces.Dal;

public class UnitOfWork : IUnitOfWork
{
    public ITranslationJobRepository TranslationJobRepository { get; }
    public ITranslatorRepository TranslatorRepository { get; }



    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}