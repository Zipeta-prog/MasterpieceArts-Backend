using OrderService.Models;
using OrderService.Models.Dto;

namespace OrderService.Services.IService
{
    public interface IOrder
    {
        Task<string> PlaceOrder(Orders order);
        Task saveChanges();
        Task<List<Orders>> GetAllOrders(Guid userId);
        Task<Orders> GetOrderById(Guid Id);
        Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto);
        Task<bool> ValidatePayments(Guid OrderId);
    }
}
