﻿using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.CheckIn;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInController : ControllerBase
    {
        [HttpPost]
        [Route("{attendeeId}")]
        [ProducesResponseType(typeof(ResponseResgisteredJson), StatusCodes.Status201Created)]        
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        public IActionResult AttendeesCheckIn([FromRoute] Guid attendeeId)
        {
            var useCase = new AttendeeChecInUseCase();

            var response = useCase.Execute(attendeeId);

            return Created(string.Empty, response);
        }
    }
}
