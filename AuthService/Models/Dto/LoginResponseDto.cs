namespace AuthService.Models.Dto
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;

        public UserDto User { get; set; } = default!;
    }
}
