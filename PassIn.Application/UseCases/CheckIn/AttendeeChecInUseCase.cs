

using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.CheckIn
{
    public class AttendeeChecInUseCase
    {

        private readonly PassInDBContext _context;
        public AttendeeChecInUseCase()
        {

            _context = new PassInDBContext();

        }
        public ResponseResgisteredJson Execute(Guid attendeeId) {
            validate(attendeeId);
            var checkIn = new Infrastructure.Entities.CheckIn()
            {
                Attendee_Id = attendeeId,
                Created_at = DateTime.UtcNow,
            };
            _context.checkIns.Add(checkIn);
            _context.SaveChanges();
            return new ResponseResgisteredJson() {
                Id = checkIn.Id,
            };        
        }

        private void validate (Guid attendeeId)
        {
            var existAttendee = _context.Attendees.Find(attendeeId);
            if (existAttendee is null) {                
                    throw new NotFoundException("An Attendee with this id dont exist.");
            }

            var existAttendeeCheckIn = _context.checkIns.Any(a => a.Attendee_Id == attendeeId);
            if (existAttendeeCheckIn)
            {
                throw new ConflictException("You can not CheckIn twice on the same Attendee");
            }

        }
    }
}
