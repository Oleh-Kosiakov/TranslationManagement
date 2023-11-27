using External.ThirdParty.Services;
using Microsoft.Extensions.Logging;
using TranslationManagement.Bll.Exceptions;
using TranslationManagement.Domain.Entities;
using TranslationManagement.Domain.Enums;
using TranslationManagement.Interfaces.Dal;
using TranslationManagement.Interfaces.Services;

namespace TranslationManagement.Bll.Services;

public class TranslationJobService : ITranslationJobService
{
    //TODO: Init from configuration
    private const decimal PricePerCharacter = 0.01m;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TranslatorService> _logger;

    private readonly INotificationService _unreliableNotificationService;

    public TranslationJobService(
        IUnitOfWork unitOfWork,
        ILogger<TranslatorService> logger,
        INotificationService unreliableNotificationService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _unreliableNotificationService = unreliableNotificationService;
    }

    public Task<IEnumerable<TranslationJob>> GetAllAsync(CancellationToken ct = default) =>
        _unitOfWork.TranslationJobRepository.GetAsync(ct: ct);

    public async Task AddNewJobAsync(TranslationJob job, CancellationToken ct)
    {
        ValidateNewJob(job);

        job.Status = TranslationJobStatus.New;
        job.Price = job.OriginalContent.Length * PricePerCharacter;

        await _unitOfWork.TranslationJobRepository.CreateAsync(job, ct).ConfigureAwait(false);
        var numberOfAfChanges = await _unitOfWork.SaveChangesAsync(ct).ConfigureAwait(false);

        var successfullySavedNewJob = HasSuccessfullySavedNewJob();

        if (successfullySavedNewJob)
        {
            _logger.LogInformation("Translation Job with id {0} was created.", job.Id);

            FireAndForgetSendNotificationAboutJobCreation(job, ct);
        }
        else
        {
            _logger.LogError("Translation Job notification creation was failed. An Unreliable Service Notification is aborted.");
            throw new EntityCreationFailedException(typeof(TranslationJob));
        }

        bool HasSuccessfullySavedNewJob()
        {
            // May not be 100% accurate in some cases. If we unsure in DB - we need some other approach. The most reliable: read the recently written job.
            return numberOfAfChanges > 0;
        }
    }

    private void FireAndForgetSendNotificationAboutJobCreation(TranslationJob job, CancellationToken ct)
    {
        // This is implementation is not production ready.I think the best option would be to add some retry logic.
        // I would do it with Polly lib, but I decided to skip it in scope of the test task.

        var sendNotificationTask = _unreliableNotificationService.SendNotification($"Job created: {job.Id}");
        Task.Run(async () =>
        {
            try
            {
                var sent = await sendNotificationTask;

                if (sent)
                {
                    _logger.LogInformation("Notification for job with id {0} was sent.", job.Id);
                }
                else
                {
                    _logger.LogWarning("There were no error, but notification for job with id {0} probably was not sent.", job.Id);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Notification Sending for job with id {0} was failed.", job.Id);
            }

        }, ct);
    }

    private void ValidateNewJob(TranslationJob job)
    {
        if (job.Price != default)
        {
            _logger.LogWarning("New translation job price is autocalculated. Passed value {0} will be ignored.", job.Price);
        }

        if (job.Status != TranslationJobStatus.New)
        {
            _logger.LogWarning("New translation job price is autoset to {0}. Passed value {1} will be ignored.",
                nameof(TranslationJobStatus.New), job.Status);
        }

        if (job.OriginalContent.Length <= 0)
        {
            _logger.LogError("New translation job OriginalContent length should be more than 0 symbols.");

            throw new ArgumentException("New Translation Job Content length should be more than 0 symbols.",
                nameof(job.OriginalContent));
        }
    }
}