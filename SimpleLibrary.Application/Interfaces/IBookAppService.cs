using FluentValidation.Results;
using SimpleLibrary.Application.ViewModels;

namespace SimpleLibrary.Application.Interfaces;

public interface IBookAppService : IDisposable
{
    Task<ValidationResult> Register(BookViewModel bookViewModel);
}
