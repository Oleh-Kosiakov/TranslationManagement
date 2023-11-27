using AutoMapper;
using TranslationManagement.Api.ViewModels;
using TranslationManagement.Domain.Entities;

namespace TranslationManagement.Api;

public class ApiMapperProfile : Profile
{
    public ApiMapperProfile()
    {
        CreateMap<CreateTranslatorRequest, Translator>();
        CreateMap<CreateTranslationJobRequest, TranslationJob>();
    }
}