namespace BiddingService.Models.Dto
{
    public class ResponseDto
    {
        public string Errormessage { get; set; } = string.Empty;

        public object Result { get; set; } = default!;

        public bool IsSuccess { get; set; } = true;
    }
}
