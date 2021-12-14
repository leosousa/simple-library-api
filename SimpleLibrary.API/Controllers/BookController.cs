using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.Identity.Authorization;
using SimpleLibrary.API.Controllers.Base;
using SimpleLibrary.Application.EventSourcedNormlizers;
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

    [AllowAnonymous]
    [HttpGet("book/{id:guid}")]
    public async Task<BookViewModel> Get(Guid id)
    {
        return await _bookAppService.GetById(id);
    }

    [CustomAuthorize("Customers", "Write")]
    [HttpPut("book")]
    public async Task<IActionResult> Put([FromBody] BookViewModel bookViewModel)
    {
        return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _bookAppService.Update(bookViewModel));
    }

    [CustomAuthorize("Customers", "Remove")]
    [HttpDelete("book")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return CustomResponse(await _bookAppService.Remove(id));
    }

    [AllowAnonymous]
    [HttpGet("book/history/{id:guid}")]
    public async Task<IList<BookHistoryData>> History(Guid id)
    {
        return await _bookAppService.GetAllHistory(id);
    }
}
