using System.ComponentModel.DataAnnotations;
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Api.ViewModels;

public class ChangeTranslationStatusRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Id is not correct.")]
    public int TranslatorId { get; set; }
    public TranslatorStatus NewTranslatorStatus { get; set; }
}