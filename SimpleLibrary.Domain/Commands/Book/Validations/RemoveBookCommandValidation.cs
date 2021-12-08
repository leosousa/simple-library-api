namespace SimpleLibrary.Domain.Commands.Book.Validations;

public class RemoveBookCommandValidation : BookValidation<RemoveBookCommand>
{
    public RemoveBookCommandValidation()
    {
        ValidateId();
    }
}
