namespace SimpleLibrary.Domain.Commands.Book.Validations;

public class RegisterNewBookCommandValidation : BookValidation<RegisterNewBookCommand>
{
    public RegisterNewBookCommandValidation()
    {
        ValidateTitle();
        ValidatePublishDate();
        ValidateIsbn();
        ValidateEdition();
    }
}
