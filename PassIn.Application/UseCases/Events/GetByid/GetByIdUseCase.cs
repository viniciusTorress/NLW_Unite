using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.GetByid
{
    public class GetByIdUseCase
    {
        public ResponseEventJson Execute(Guid eventId) 
        {
            var dbContext = new PassInDBContext();

            var eventEntity = dbContext.Events.Include(i => i.Attendees).FirstOrDefault(f => f.Id == eventId);
            
            if (eventEntity is null)
                throw new NotFoundException("An event with this id dont exist.");

            return new ResponseEventJson
            {
                Id = eventEntity.Id,
                Title = eventEntity.Title,
                Details = eventEntity.Details,
                MaximumAttendees = eventEntity.Maximum_Attendees,
                AttendeesAmount = eventEntity.Attendees.Count()
            };

        }
    }
}
