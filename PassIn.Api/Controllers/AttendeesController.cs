using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Attendees;
using PassIn.Application.UseCases.Events.Attendee;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendeesController : ControllerBase
    {
        [HttpPost]
        [Route("{eventId}/register")]
        [ProducesResponseType(typeof(ResponseResgisteredJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        public IActionResult RegisterAttendee([FromRoute] Guid eventId, [FromBody] RequestRegisterEventJson request)
        {

            var useCase = new RegisterAttendeeEventOnUseCase();
            var response = useCase.execute(eventId, request);
            return Created(string.Empty, response);

        }

        [HttpGet]
        [Route("{eventId}")]
        [ProducesResponseType(typeof(ResponseAllAttendeesjson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetAll([FromRoute] Guid eventId) {
            var useCase = new GetAllAttenddessByEventIdUseCase();
            var response = useCase.Execute(eventId);
            return Ok(response);
        }    
    }
}
