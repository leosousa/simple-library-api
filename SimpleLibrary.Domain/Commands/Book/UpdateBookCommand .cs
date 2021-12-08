using SimpleLibrary.Domain.Commands.Book.Validations;

namespace SimpleLibrary.Domain.Commands.Book;

public class UpdateBookCommand : BookCommand
{
    public UpdateBookCommand(Guid id, string title, DateTime publishDate, string isbn, int edition)
    {
        Id = id;
        Title = title;
        PublishDate = publishDate;
        ISBN = isbn;
        Edition = edition;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateBookCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
