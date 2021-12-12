using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.Identity.Authorization;
using SimpleLibrary.API.Controllers.Base;
using SimpleLibrary.Application.Interfaces;
using SimpleLibrary.Application.ViewModels;

namespace SimpleLibrary.API.Controllers;

public class BookController : ApiController
{
    private readonly IBookAppService _bookAppService;

    public BookController(IBookAppService bookAppService)
    {
        _bookAppService = bookAppService;
    }

    [CustomAuthorize("Customers", "Write")]
    [HttpPost("books")]
    public async Task<IActionResult> Post([FromBody] BookViewModel bookViewModel)
    {
        return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _bookAppService.Register(bookViewModel));
    }

    [AllowAnonymous]
    [HttpGet("book")]
    public async Task<IEnumerable<BookViewModel>> Get()
    {
        return await _bookAppService.GetAll();
    }
}
