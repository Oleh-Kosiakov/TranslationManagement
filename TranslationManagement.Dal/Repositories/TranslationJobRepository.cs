using System.Linq.Expressions;
using AutoMapper;
using TranslationManagement.Dal.EF;
using TranslationManagement.Domain.Entities;
using TranslationManagement.Interfaces.Dal.Repositories;

namespace TranslationManagement.Dal.Repositories;

/// <summary>
/// Unfortunately, in this approach we will not be able to mock the repository.
/// </summary>
public class TranslationJobRepository : ITranslationJobRepository
{
    private readonly TranslationsDbContext _translationsDbContext;
    private readonly IMapper _mapper;

    public TranslationJobRepository(TranslationsDbContext translationsDbContext, IMapper mapper)
    {
        _translationsDbContext = translationsDbContext;
        _mapper = mapper;
    }

    public Task<IEnumerable<TranslationJob>> GetAsync(Expression<Func<TranslationJob, bool>>? predicate = default, CancellationToken ct = default)
    {
        if (predicate == null)
        {
            return Task.FromResult(_translationsDbContext.TranslationJobsSet.ToList().AsEnumerable());
        }

        return Task.FromResult(_translationsDbContext.TranslationJobsSet.Where(predicate).ToList().AsEnumerable());
    }

    public Task CreateAsync(TranslationJob entity, CancellationToken ct)
    {
         _translationsDbContext.Add(entity);

         return Task.CompletedTask;
    }

    public Task UpdateAsync(TranslationJob entity, CancellationToken ct)
    {
        var trackedEntity = _translationsDbContext.Find<TranslationJob>(entity.Id);

        if (trackedEntity != null)
        {
            _mapper.Map(entity, trackedEntity);
        }
        else
        {
            throw new InvalidOperationException($"{nameof(TranslationJob)} with Id {entity.Id} was not found.");
        }

        _translationsDbContext.Update(entity);

        return Task.CompletedTask;
    }
}