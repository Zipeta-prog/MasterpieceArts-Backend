using AutoMapper;
using BiddingService.Models;
using BiddingService.Models.Dto;

namespace BiddingService.Profiles
{
    public class BidProfile : Profile
    {
        public BidProfile()
        {
            CreateMap<AddBidDto, Bids>().ReverseMap();
        }
    }
}
