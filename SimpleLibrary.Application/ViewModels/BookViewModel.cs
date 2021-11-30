using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLibrary.Application.ViewModels;

public class BookViewModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public DateTime PublishedDate { get; set; }

    public string ISBN { get; set; }

    public int Edition { get; set; }
}
