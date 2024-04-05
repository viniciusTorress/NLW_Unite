using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.Attendee;
using PassIn.Application.UseCases.Events.GetByid;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;

namespace PassIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseResgisteredJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestEventJson request)
        {
            var useCase = new RegisterEventUsecase();

            var response = useCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetById([FromRoute] Guid id) {

            var useCase = new GetByIdUseCase();

            var response = useCase.Execute(id);

            return Ok(response);

        }


        [HttpPost]
        [Route("{eventId}/register")]
        [ProducesResponseType(typeof(ResponseResgisteredJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        public IActionResult RegisterAttendee([FromRoute] Guid eventId,[FromBody] RequestRegisterEventJson request) {

            var useCase = new RegisterAttendeeEventonUseCase();
            var response = useCase.execute(eventId, request);
            return Created(string.Empty, response);
        
        }

    }
}
