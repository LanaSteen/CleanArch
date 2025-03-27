using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands.Manager;
using MyApp.Application.DTOs.Manager;
using MyApp.Application.Exceptions;
using MyApp.Application.Queries.Manager;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Api.Controllers
{
    [Route("api/hotel/[controller]")]

    public class ManagerController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public ManagerController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }


        [HttpPost]
        //[Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateManagerAsync([FromBody] CreateManagerRequest managerRequest)
        {
            try
            {
                var result = await _sender.Send(new CreateManagerCommand(managerRequest));
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("All")]
        //[Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAllManagersAsync()
        {
            var managers = await _sender.Send(new GetAllManagersQuery());
            var managersDto = _mapper.Map<List<ManagerDto>>(managers);
            return Ok(managersDto);
        }

        [HttpGet("{id}")]
        //[Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetManagerByIdAsync([FromRoute] int id)
        {
            var manager = await _sender.Send(new GetManagerByIdQuery(id));
            if (manager == null)
            {
                return NotFound();  
            }

            var managerDto = _mapper.Map<ManagerDto>(manager);
            return Ok(managerDto);  
        }


        [HttpPut("{managerId}")]
        //[Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateManagerAsync([FromRoute] int managerId, [FromBody] UpdateManagerRequest managerRequest)
        {
            try
            {
                var result = await _sender.Send(new UpdateManagerCommand(managerId, managerRequest));
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        //[Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteManagerAsync([FromRoute] int id)
        {
            var result = await _sender.Send(new DeleteManagerCommand(id));

            if (!result)
            {
                return NotFound();  
            }

            return NoContent();  
        }
    }
}
