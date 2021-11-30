using AutoMapper;
using SimpleLibrary.Application.ViewModels;
using SimpleLibrary.Domain.Entities;

namespace SimpleLibrary.Application.AutoMapper;

public class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile()
    {
        CreateMap<Book, BookViewModel>();
    }
}
