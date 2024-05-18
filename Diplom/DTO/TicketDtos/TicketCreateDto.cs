using Diplom.Data;
using Diplom.Entities;

namespace Diplom.DTO.TicketDtos
{
    public class TicketCreateDto
    {
        public DateTime Date { get; set; }
        public Guid RouteId { get; set; }
        public Guid ScheduleId { get; set; }

    }
}
