using System.ComponentModel.DataAnnotations;
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Api.ViewModels;

public class CreateTranslatorRequest
{
    [Required]
    [StringLength(300)]
    public string Name { get; set; }

    [Required]
    [StringLength(30)]
    public string HourlyRate { get; set; }

    public TranslatorStatus Status { get; set; }
    
    [Required]
    [StringLength(30)]
    public string CreditCardNumber { get; set; }
}