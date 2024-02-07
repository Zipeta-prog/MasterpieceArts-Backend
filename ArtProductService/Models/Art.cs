namespace ArtProductService.Models
{
    public class Art
    {
        public Guid Id { get; set; }
        public Guid SellerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime StopTime { get; set; }
        public int StartPrice { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Status { get; set; } = "Open";
    }
}
