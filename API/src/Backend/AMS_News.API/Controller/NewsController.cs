using AMS_News.API.Attribute;
using AMS_News.Application.UseCases.News;
using AMS_News.Application.UseCases.News.Delete;
using AMS_News.Application.UseCases.News.Edit;
using AMS_News.Application.UseCases.News.Get;
using AMS_News.Application.UseCases.News.GetById;
using AMS_News.Application.UseCases.News.GetByUser;
using AMS_News.Application.UseCases.News.Register;
using AMS_News.Communication.Request.News;
using AMS_News.Communication.Response.News;
using AMS_News.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AMS_News.API.Controller;

public class NewsController : AmsNewsBaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseNewsJson),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorMessage),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetNews([FromServices] IGetNewsUseCase useCase)
    {
        var news = await useCase.Execute();
        return Ok(news);
    }
    [HttpPost]
    [ProducesResponseType(typeof(ResponseNewsJson),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorMessage),StatusCodes.Status404NotFound)]
    [AuthenticatedUser]
    public async Task<IActionResult> Register([FromServices] IRegisterNewsUseCase useCase,[FromForm] RequestNewsJson request)
    {
        var news = await useCase.Execute(request);
        return Ok(news);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorMessage),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorMessage),StatusCodes.Status401Unauthorized)]
    [AuthenticatedUser]
    public async Task<IActionResult> Edit([FromServices] IEditNewsUseCase useCase,[FromForm] RequestNewsJson request,long id)
    {
         await useCase.ExecuteAsync(id,request);
        return NoContent();
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseNewsJson),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorMessage),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorMessage),StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetById([FromServices] IGetByIdNewsUseCase useCase,[FromRoute] long id)
    {
        var news = await useCase.ExecuteAsync(id);
        return Ok(news);
    }
    [HttpGet("get-by-user")]
    [ProducesResponseType(typeof(List<ResponseNewsJson>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorMessage),StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorMessage),StatusCodes.Status401Unauthorized)]
    [AuthenticatedUser]
    public async Task<IActionResult> GetByUser([FromServices] IGetByUserNewsUseCase useCase)
    {
        var news = await useCase.ExecuteAsync();
        return Ok(news);
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorMessage),StatusCodes.Status401Unauthorized)]
    [AuthenticatedUser]
    public async Task<IActionResult> GetByUser([FromServices] IDeleteNewsUseCase useCase,[FromRoute] long id)
    {
        await useCase.ExecuteAsync(id);
        return NoContent();
    }
    
}