﻿using TranslationManagement.Domain.Entities;
using TranslationManagement.Domain.Enums;

namespace TranslationManagement.Interfaces.Services;

public interface ITranslatorService
{
    Task<IEnumerable<Translator>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Translator>> GetTranslatorsByName(string name, CancellationToken ct = default);
    Task AddTranslator(Translator translator, CancellationToken ct = default);

    Task UpdateTranslatorStatus(
        int translatorId,
        TranslatorStatus translatorStatus,
        CancellationToken ct = default);
}