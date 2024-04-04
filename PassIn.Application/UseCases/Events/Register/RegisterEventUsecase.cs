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
        public ResponseResgisteredEventJson Execute(RequestEventJson request) {
            validate(request);

            var dbContext = new PassInDBContext();
            var eventEntity = new Event(){
                Title = request.Title,
                Maximum_Attendes = request.MaximumAttendees,
                Details = request.Details,
                Slug = request.Title.ToLower().Replace(" ", "-")
            };

            dbContext.Events.Add(eventEntity);
            dbContext.SaveChanges();
            return new ResponseResgisteredEventJson
            {
                Id = eventEntity.Id
            };
        
        }
        private void validate(RequestEventJson request) { 
            if (request.MaximumAttendees <= 0) {
                throw new PassInException("The Maximmun attendes is invalid");
            }
            if (string.IsNullOrWhiteSpace(request.Title)) {
                throw new PassInException("The Title is invalid");
            }
            if (string.IsNullOrWhiteSpace(request.Details))
            {
                throw new PassInException("The Details is invalid");
            }
        }

    }
}
