using PassIn.Communication.Responses;
using PassIn.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PassIn.Exceptions;

namespace PassIn.Application.UseCases.Attendees
{

    public class GetAllAttenddessByEventIdUseCase
    {
        private readonly PassInDBContext _context;
        public GetAllAttenddessByEventIdUseCase()
        {

            _context = new PassInDBContext();

        }
        public ResponseAllAttendeesjson Execute(Guid eventId) {
            var lstAttendees = _context.Events.Include(i => i.Attendees).ThenInclude(attendee => attendee.CheckIn).FirstOrDefault(f => f.Id == eventId );
            if (lstAttendees is null)
                throw new NotFoundException("An event with this id dont exist.");
            return new ResponseAllAttendeesjson() {
                Attendees =  lstAttendees.Attendees.Select(s => new ResponseAttendeeJson {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    CreatedAt = s.Created_At,
                    CheckedInAt =  s.CheckIn?.Created_at,
                }).ToList(),
            };
        }
    }
}
