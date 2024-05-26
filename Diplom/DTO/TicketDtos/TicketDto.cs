using Diplom.Data;
using Diplom.Entities;

namespace Diplom.DTO.TicketDtos
{
    public class TicketDto
    {
        public string Date { get; set; }
        public string Start {  get; set; }
        public string End { get; set; }
        public string QRUrl { get; set; }
        public string Price { get; set; }
        public string Schedule {get; set;}

    }
}
