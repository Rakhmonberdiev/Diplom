

namespace Diplom.DTO.RouteEnDtos
{
    public class RouteCreateDto
    {
        public int Price { get; set; }
        public Guid StartPointId { get; set; }
        public Guid EndPointId { get; set; }
    }
}
