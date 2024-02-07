namespace OrderService.Models.Dto
{
    public class OrderDto
    {
        public Guid BidId { get; set; }
        public Guid BidderId { get; set; }
        public Guid ArtId { get; set; }
        public Double TotalAmount { get; set; }
    }
}
