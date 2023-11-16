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
        public async Task<ActionResult<IEnumerable<Translator>>> GetAll(CancellationToken ct)
        {
            return Ok(await _translatorService.GetAllAsync(ct));
        }

        [HttpGet("filtered")]
        public async Task<ActionResult<IEnumerable<Translator>>> GetFiltered([FromQuery] string name, CancellationToken ct)
        {
            return Ok(await _translatorService.GetTranslatorsByName(name, ct));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTranslatorRequest request, CancellationToken ct)
        {
            if (ModelHasValidationErrors(out var validationActionResult))
            {
                return validationActionResult;
            }

            var translatorEntity = _mapper.Map<CreateTranslatorRequest, Translator>(request);

            await _translatorService.AddTranslator(translatorEntity, ct);

            return new StatusCodeResult(StatusCodes.Status201Created);
        }

        [HttpPatch("status")]
        public async Task<IActionResult> UpdateTranslatorStatus(
            [FromBody] ChangeTranslationStatusRequest request,
            CancellationToken ct)
        {
            if (ModelHasValidationErrors(out var validationActionResult))
            {
                return validationActionResult;
            }

            await _translatorService.UpdateTranslatorStatus(request.TranslatorId, request.NewTranslatorStatus, ct);

            return new StatusCodeResult(StatusCodes.Status202Accepted);
        }
    }
}
