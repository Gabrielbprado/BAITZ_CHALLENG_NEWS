using AMS_News.API.Attribute;
using AMS_News.Application.UseCases.Like.DoLike;
using AMS_News.Application.UseCases.Like.GetMoreLiked;
using AMS_News.Communication.Request.Like;
using AMS_News.Communication.Response.News;
using AMS_News.Domain.Entities;
using AMS_News.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AMS_News.API.Controller;

public class LikeController : AmsNewsBaseController
{
    [HttpPost("{id}")] 
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status401Unauthorized)]
    [AuthenticatedUser]
    public async Task<IActionResult> DoLike([FromServices] IDoLikeUseCase useCase,[FromRoute] long id)
    {
        await useCase.ExecuteAsync(id);
        return Created();
    }
    [HttpGet] 
    [ProducesResponseType(typeof(ResponseNewsJson),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMoreLiked([FromServices] IGetMoreLiked useCase)
    {
        var news = await useCase.ExecuteAsync();
        return Ok(news);
    }
}