using SimpleLibrary.Domain.Commands.Book.Validations;

namespace SimpleLibrary.Domain.Commands.Book;

public class RemoveBookCommand : BookCommand
{
    public RemoveBookCommand(Guid id)
    {
        Id = id;
        AggregateId = id;
    }

    public override bool IsValid()
    {
        ValidationResult = new RemoveBookCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
