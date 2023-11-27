#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.ComponentModel.DataAnnotations;

namespace TranslationManagement.Api.ViewModels;

public class CreateTranslationJobRequest
{
    [Required]
    [StringLength(300)]
    public string CustomerName { get; set; }

    [Required]
    [StringLength(100000)]
    public string OriginalContent { get; set; }
}