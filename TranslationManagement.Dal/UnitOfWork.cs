using AutoMapper;
using TranslationManagement.Dal.EF;
using TranslationManagement.Dal.Repositories;
using TranslationManagement.Interfaces.Dal;
using TranslationManagement.Interfaces.Dal.Repositories;

namespace TranslationManagement.Dal;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly TranslationsDbContext _translationsDbContext;
    private readonly IMapper _mapper;

    public UnitOfWork(TranslationsDbContext translationsDbContext, IMapper mapper)
    {
        _translationsDbContext = translationsDbContext;
        _mapper = mapper;
    }

    private ITranslationJobRepository? _translationJobRepository;
    public ITranslationJobRepository TranslationJobRepository =>
        _translationJobRepository ??= new TranslationJobRepository(_translationsDbContext, _mapper);

    private ITranslatorRepository? _translatorRepository;
    public ITranslatorRepository TranslatorRepository =>
        _translatorRepository ??= new TranslatorRepository(_translationsDbContext, _mapper);

    public async Task<int> SaveChangesAsync(CancellationToken ct = default) => 
        await _translationsDbContext.SaveChangesAsync(ct);

    public void Dispose()
    {
        //We may need to use more sophisticated disposals depending of the needs and context.
        _translationsDbContext.Dispose();
    }
}