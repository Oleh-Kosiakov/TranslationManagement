using System.Linq.Expressions;
using AutoMapper;
using TranslationManagement.Dal.EF;
using TranslationManagement.Domain.Entities;
using TranslationManagement.Interfaces.Dal.Repositories;

namespace TranslationManagement.Dal.Repositories;

public class TranslatorRepository : ITranslatorRepository
{
    private readonly TranslationsDbContext _translationsDbContext;
    private readonly IMapper _mapper;

    public TranslatorRepository(TranslationsDbContext translationsDbContext, IMapper mapper)
    {
        _translationsDbContext = translationsDbContext;
        _mapper = mapper;
    }

    public Task<IEnumerable<Translator>> GetAsync(
        Expression<Func<Translator, bool>>? predicate = default,
        CancellationToken ct = default)
    {
        if (predicate == null)
        {
            return Task.FromResult(_translationsDbContext.TranslatorSet.ToList().AsEnumerable());
        }

        return Task.FromResult(_translationsDbContext.TranslatorSet.Where(predicate).ToList().AsEnumerable());
    }

    public Task CreateAsync(Translator entity, CancellationToken ct = default)
    {
        _translationsDbContext.Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Translator entity, CancellationToken ct = default)
    {
        var trackedEntity = _translationsDbContext.Find<Translator>(entity.Id);

        if (trackedEntity != null)
        {
            _mapper.Map(entity, trackedEntity);
        }
        else
        {
            throw new InvalidOperationException($"{nameof(Translator)} with Id {entity.Id} was not found.");
        }

        _translationsDbContext.Update(entity);

        return Task.CompletedTask;
    }
}