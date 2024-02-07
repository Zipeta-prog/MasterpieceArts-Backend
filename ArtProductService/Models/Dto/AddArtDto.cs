namespace ArtProductService.Models.Dto
{
    public class AddArtDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime StopTime { get; set; }
        public double StartPrice { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
