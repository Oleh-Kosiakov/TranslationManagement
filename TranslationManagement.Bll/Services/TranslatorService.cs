using Microsoft.Extensions.Logging;
using TranslationManagement.Bll.Exceptions;
using TranslationManagement.Domain.Entities;
using TranslationManagement.Domain.Enums;
using TranslationManagement.Interfaces.Dal;
using TranslationManagement.Interfaces.Services;

namespace TranslationManagement.Bll.Services;

public class TranslatorService : ITranslatorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TranslatorService> _logger;

    public TranslatorService(IUnitOfWork unitOfWork, ILogger<TranslatorService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public Task<IEnumerable<Translator>> GetAllAsync(CancellationToken ct = default) =>
        _unitOfWork.TranslatorRepository.GetAsync(ct: ct);

    public Task<IEnumerable<Translator>> GetTranslatorsByNameAsync(string name, CancellationToken ct = default) =>
        _unitOfWork.TranslatorRepository.GetAsync(t => string.Compare(t.Name, name, StringComparison.OrdinalIgnoreCase) == 0, ct);

    public async Task AddTranslatorAsync(Translator translator, CancellationToken ct = default)
    {
        await _unitOfWork.TranslatorRepository.CreateAsync(translator, ct).ConfigureAwait(false);
        await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);

        _logger.LogInformation("Translator entity was created.");
    }

    public async Task UpdateTranslatorStatusAsync(
        int translatorId,
        TranslatorStatus translatorStatus,
        CancellationToken ct = default)
    {
        // I had to load it from DB first, actually loading it from db twice for update.
        // In case this is frequent operation, I would implement dedicated method in repository.

        _logger.LogInformation("User status update request: {NewStatus} for user {Id}", translatorStatus, translatorId);

        var translatorLists =
            await _unitOfWork.TranslatorRepository
            .GetAsync(t => t.Id == translatorId, ct)
            .ConfigureAwait(false);
        var translator = translatorLists.SingleOrDefault();

        if (translator == null)
        {
            _logger.LogError("Translator with Id {Id} was not found.", translatorId);

            throw new ResourceNotFoundException(typeof(Translator));
        }
        _logger.LogInformation("Translator with Id {Id} was found. Trying to change status...", translatorId);

        translator.Status = translatorStatus;

        await _unitOfWork.TranslatorRepository.UpdateAsync(translator, ct).ConfigureAwait(false);
        await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);

        _logger.LogInformation("Status of translator with Id {Id} has changed to {newStatus}.", translator, translatorStatus);
    }
}