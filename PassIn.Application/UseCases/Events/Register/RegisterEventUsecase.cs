using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Application.UseCases.Events.Register
{
    public class RegisterEventUsecase
    {
        public ResponseResgisteredJson Execute(RequestEventJson request) {
            validate(request);

            var dbContext = new PassInDBContext();
            var eventEntity = new Event(){
                Title = request.Title,
                Maximum_Attendees = request.MaximumAttendees,
                Details = request.Details,
                Slug = request.Title.ToLower().Replace(" ", "-")
            };

            dbContext.Events.Add(eventEntity);
            dbContext.SaveChanges();
            return new ResponseResgisteredJson
            {
                Id = eventEntity.Id
            };
        
        }
        private void validate(RequestEventJson request) { 
            if (request.MaximumAttendees <= 0) {
                throw new ErrorOnValidationException("The Maximmun attendes is invalid");
            }
            if (string.IsNullOrWhiteSpace(request.Title)) {
                throw new ErrorOnValidationException("The Title is invalid");
            }
            if (string.IsNullOrWhiteSpace(request.Details))
            {
                throw new ErrorOnValidationException("The Details is invalid");
            }
        }

    }
}
