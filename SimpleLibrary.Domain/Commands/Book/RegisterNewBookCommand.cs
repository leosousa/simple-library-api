using SimpleLibrary.Domain.Commands.Book.Validations;

namespace SimpleLibrary.Domain.Commands.Book;

public class RegisterNewBookCommand : BookCommand
{
    public RegisterNewBookCommand(string title, DateTime publishDate, string isbn, int edition)
    {
        Title = title;
        PublishDate = publishDate;
        ISBN = isbn;
        Edition = edition;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterNewBookCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
