using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLibrary.Domain.Commands.Book.Validations;

public class UpdateBookCommandValidation : BookValidation<UpdateBookCommand>
{
    public UpdateBookCommandValidation()
    {
        ValidateId();
        ValidateTitle();
        ValidatePublishDate();
        ValidateIsbn();
        ValidateEdition();
    }
}
