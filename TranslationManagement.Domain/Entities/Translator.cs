using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Domain.Entities;

public class Translator
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal HourlyRate { get; set; }
    public TranslatorStatus Status { get; set; }

    // Storing Credit Card number like this? Common
    public string CreditCardNumber { get; set; }
}