using FluentValidation;

namespace SimpleLibrary.Domain.Commands.Book.Validations;

public class BookValidation<TCommand> : AbstractValidator<TCommand> where TCommand : BookCommand
{
    protected void ValidateTitle()
    {
        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("O título precisa ser informado.")
            .Length(2, 150).WithMessage("O título precisa ter entre 3 e 255 caracteres.");
    }

    protected void ValidatePublishDate()
    {
        RuleFor(c => c.PublishDate)
            .NotEmpty()
            .WithMessage("A data de publicação precisa ser informada.");
    }

    protected void ValidateIsbn()
    {
        RuleFor(c => c.ISBN)
            .NotEmpty().WithMessage("O ISBN precisa ser informado.")
            .Length(13)
            .WithMessage("O ISBN precisa ter 13 caracteres.");
    }

    protected void ValidateEdition()
    {
        RuleFor(c => c.Edition)
            .NotEmpty().WithMessage("O número da edição precisa ser informado.");
    }
}
