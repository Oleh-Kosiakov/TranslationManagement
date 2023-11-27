using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TranslationManagement.Api.ViewModels;
using TranslationManagement.Domain.Entities;
using TranslationManagement.Interfaces.Services;

namespace TranslationManagement.Api.Controllers
{
    //TODO: Add Validation, Add configuration, Add Logging
    [Route("api/[controller]")]
    [ApiController]
    public class TranslatorController : BaseApiController
    {
        private readonly ITranslatorService _translatorService;
        private readonly IMapper _mapper;

        public TranslatorController(ITranslatorService translatorService, IMapper mapper)
        {
            _translatorService = translatorService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Translator>>> GetAllAsync(CancellationToken ct)
        {
            return Ok(await _translatorService.GetAllAsync(ct));
        }

        //TODO: Introduce support of the filtering via RESTful naming conventions
        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<Translator>>> GetFilteredAsync([FromQuery] string name, CancellationToken ct)
        {
            return Ok(await _translatorService.GetTranslatorsByNameAsync(name, ct));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTranslatorRequest request, CancellationToken ct)
        {
            if (ModelHasValidationErrors(out var validationActionResult))
            {
                return validationActionResult;
            }

            var translatorEntity = _mapper.Map<CreateTranslatorRequest, Translator>(request);

            await _translatorService.AddTranslatorAsync(translatorEntity, ct);

            return new StatusCodeResult(StatusCodes.Status201Created);
        }

        [HttpPatch("status")]
        public async Task<IActionResult> UpdateTranslatorStatusAsync(
            [FromBody] ChangeTranslationStatusRequest request,
            CancellationToken ct)
        {
            if (ModelHasValidationErrors(out var validationActionResult))
            {
                return validationActionResult;
            }

            await _translatorService.UpdateTranslatorStatusAsync(request.TranslatorId, request.NewTranslatorStatus, ct);

            return new StatusCodeResult(StatusCodes.Status202Accepted);
        }
    }
}
