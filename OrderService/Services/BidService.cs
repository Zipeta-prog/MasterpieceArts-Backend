using Newtonsoft.Json;
using OrderService.Models.Dto;
using OrderService.Services.IService;

namespace OrderService.Services
{
    public class BidService : IBid
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BidService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<BidDto> GetBidById(string Id)
        {
            var client = _httpClientFactory.CreateClient("Bid");
            var response = await client.GetAsync(Id);
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.Result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<BidDto>(responseDto.Result.ToString());
            }
            return new BidDto();
        }
    }
}
