using Diplom.Data;

namespace Diplom.Entities
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
     
        public RouteEn Route { get; set; }
        public Guid RouteId { get; set; }
        public Schedule Schedule { get; set; }
        public Guid ScheduleId { get; set; }
        public AppUser AppUser { get; set; }
        public string UserId { get; set; }
    }
}
