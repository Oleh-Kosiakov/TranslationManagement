using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TranslationManagement.Api.ViewModels;
using TranslationManagement.Domain.Entities;
using TranslationManagement.Interfaces.Services;

namespace TranslationManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationJobController : BaseApiController
    {
        private readonly ITranslationJobService _translationJobService;
        private readonly IMapper _mapper;

        public TranslationJobController(ITranslationJobService translationJobService, IMapper mapper)
        {
            _translationJobService = translationJobService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TranslationJob>>> GetAllAsync(CancellationToken ct) => 
            Ok(await _translationJobService.GetAllAsync(ct));

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTranslationJobRequest request, CancellationToken ct)
        {
            if (ModelHasValidationErrors(out var validationActionResult))
            {
                return validationActionResult;
            }

            var translationJob = _mapper.Map<CreateTranslationJobRequest, TranslationJob>(request);

            await _translationJobService.AddNewJobAsync(translationJob, ct);

            return new StatusCodeResult(StatusCodes.Status201Created);
        }
    }
}
