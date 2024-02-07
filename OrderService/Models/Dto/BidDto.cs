namespace OrderService.Models.Dto
{
    public class BidDto
    {
        public Guid Id { get; set; }
        public Guid ArtId { get; set; }
        public string? ArtName { get; set; }
        public Guid BidderId { get; set; }
        public string? BidderName { get; set; }
        public double BidAmmount { get; set; }
        public DateTime BidDate { get; set; }
    }
}
