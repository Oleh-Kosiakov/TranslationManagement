using Microsoft.AspNetCore.Mvc;

namespace TranslationManagement.Api.Controllers;

public class BaseApiController : ControllerBase
{
    protected bool ModelHasValidationErrors(out IActionResult validationActionResult)
    {
        validationActionResult = Ok();

        if (!ModelState.IsValid)
        {
            var errors = new Dictionary<string, string[]>();
            foreach (var entry in ModelState)
            {
                if (entry.Value.Errors.Count > 0)
                {
                    var errorMessages = entry.Value.Errors.Select(error => error.ErrorMessage).ToArray();
                    errors.Add(entry.Key, errorMessages);
                }
            }
            validationActionResult = BadRequest(new { errors = errors });

            return true;
        }

        return false;
    }
}