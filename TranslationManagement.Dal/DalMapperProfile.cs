using AutoMapper;
using TranslationManagement.Domain.Entities;

namespace TranslationManagement.Dal;

public class DalMapperProfile : Profile
{
    public DalMapperProfile()
    {
        CreateMap<TranslationJob, TranslationJob>();
        CreateMap<Translator, Translator>();
    }
}