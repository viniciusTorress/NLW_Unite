using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Application.UseCases.Events.Attendee
{
    public class RegisterAttendeeEventOnUseCase
    {
        private readonly PassInDBContext _context;
        public RegisterAttendeeEventOnUseCase()
        {

            _context = new PassInDBContext();

        }
        public ResponseResgisteredJson execute (Guid eventId, RequestRegisterEventJson request) {
            validate(eventId, request);
            var attendee = new Infrastructure.Entities.Attendee()
            {
                Email = request.Email,
                Name = request.Name,
                Event_Id = eventId,
                Created_At= DateTime.UtcNow,
            };
            
            _context.Add(attendee);
            _context.SaveChanges();

            return new ResponseResgisteredJson
            {
                Id = attendee.Id
            };
        }

        private void validate(Guid eventId, RequestRegisterEventJson request) {
            var eventExist = _context.Events.Find(eventId);

            if(eventExist is null)                
                    throw new NotFoundException("An event with this id dont exist.");
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ErrorOnValidationException("The Name is invalid");
            }
            if (EmailValid(request.Email) == false)
            {
                throw new ErrorOnValidationException("The Email is invalid");
            }
            if (IsAtteendeeResgistered(request.Email, eventId) == true)
            {
                throw new ConflictException("You can not register twice on the same event");
            }

            var attendeesForEvent = _context.Attendees.Count(c => c.Event_Id == eventId);
            if (attendeesForEvent == eventExist.Maximum_Attendees) {
                throw new ErrorOnValidationException("There is not room for this event");
            }
        }

        private bool EmailValid(string email) {
            try {
            
                new MailAddress(email);
                return true;
            }catch
            {
                return false;
            }
        }

        private bool IsAtteendeeResgistered(string email, Guid eventId) {
             return _context.Attendees.Any(a => a.Event_Id == eventId && a.Email.Equals(email));
        }
    }
}
