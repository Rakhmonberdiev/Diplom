namespace Diplom.Entities
{
    public class RouteEn
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        public Guid StartPointId { get; set; }
        public DistrictsEn StartPoint { get; set; }
        public Guid EndPointId { get; set; }
        public DistrictsEn EndPoint { get; set; }
        public DateTime Created {  get; set; } = DateTime.UtcNow;
    }
}
