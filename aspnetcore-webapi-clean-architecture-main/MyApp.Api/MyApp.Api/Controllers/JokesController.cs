using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands;
using MyApp.Application.Queries;
using MyApp.Core.Entities;

namespace MyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetSomeFunController(ISender sender) : ControllerBase
    {
    
        [HttpGet("joke")]
        public async Task<IActionResult> GetJoke()
        {
            var result = await sender.Send(new GetJokeQuery());
            return Ok(result);
        }
    }
}
