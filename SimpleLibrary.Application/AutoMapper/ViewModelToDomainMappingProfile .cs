using AutoMapper;
using SimpleLibrary.Application.ViewModels;
using SimpleLibrary.Domain.Commands.Book;

namespace SimpleLibrary.Application.AutoMapper;

public class ViewModelToDomainMappingProfile : Profile
{
    public ViewModelToDomainMappingProfile()
    {
        CreateMap<BookViewModel, RegisterNewBookCommand>()
            .ConstructUsing(b => new RegisterNewBookCommand(b.Title, b.PublishedDate, b.ISBN, b.Edition));
    }
}
