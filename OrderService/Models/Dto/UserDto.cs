namespace OrderService.Models.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
