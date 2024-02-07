using AutoMapper;
using OrderService.Models;
using OrderService.Models.Dto;

namespace OrderService.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDto, Orders>().ReverseMap();
        }
    }
}
