using AMS_News.Application.Login;
using AMS_News.Communication.Request;
using AMS_News.Communication.Response.User;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AMS_News.API.Controller;

public class LoginController : AmsNewsBaseController
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserJson),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    
    public async Task<IActionResult> Login([FromBody] RequestLoginUserJson request,[FromServices] IDoLoginUseCase useCase)
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }
}