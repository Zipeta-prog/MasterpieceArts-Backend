using OrderService.Models.Dto;

namespace OrderService.Services.IService
{
    public interface IBid
    {
        Task<BidDto> GetBidById(string Id);
    }
}
