#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.ComponentModel.DataAnnotations;
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Api.ViewModels;

public class CreateTranslatorRequest
{
    [Required]
    [StringLength(300)]
    public string Name { get; init; }

    [Required]
    [Range(0, int.MaxValue)]
    public decimal HourlyRate { get; init; }

    [Required]
    public TranslatorStatus Status { get; init; }
    
    [Required]
    [StringLength(30)]
    public string CreditCardNumber { get; init; }
}