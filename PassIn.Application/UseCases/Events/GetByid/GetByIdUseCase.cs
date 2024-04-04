using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Application.UseCases.Events.GetByid
{
    public class GetByIdUseCase
    {
        public ResponseEventJson Execute(Guid id) 
        {
            var dbContext = new PassInDBContext();

            var eventEntity = dbContext.Events.FirstOrDefault(f => f.Id == id);

            if (eventEntity is null)
                throw new PassInException("An event with this id dont exist.");

            return new ResponseEventJson
            {
                Id = eventEntity.Id,
                Title = eventEntity.Title,
                Details = eventEntity.Details,
                MaximumAttendees = eventEntity.Maximum_Attendes,
                AttendeesAmount = -1
            };

        }
    }
}
