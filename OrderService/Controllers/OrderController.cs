using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Models.Dto;
using OrderService.Services.IService;
using System.Security.Claims;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBid _bidService;
        private readonly IArt _artService;
        private readonly IOrder _orderService;
        private readonly ResponseDto _responseDto;

        public OrderController(IMapper mapper, IBid bid, IArt art,
            IOrder order)
        {
            _artService = art;
            _mapper = mapper;
            _bidService = bid;
            _orderService = order;
            _responseDto = new ResponseDto();
        }

        [HttpPost("{BidId}")]
        public async Task<ActionResult<ResponseDto>> PlaceOrder(OrderDto dto, string BidId)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _responseDto.Errormessage = "Please login";
                return Unauthorized(_responseDto);
            }

            var bid = await _bidService.GetBidById(BidId);

            if (string.IsNullOrWhiteSpace(bid.ArtName))
            {
                _responseDto.Errormessage = "Items Not Found";
                return NotFound(_responseDto);
            }

            var order = _mapper.Map<Orders>(dto);

            order.BidId = bid.Id;
            order.BidderId = new Guid(UserId);
            order.TotalAmount = bid.BidAmmount;




            var res = await _orderService.PlaceOrder(order);
            _responseDto.Result = res;
            return Ok(_responseDto);
        }

        [HttpGet]

        public async Task<ActionResult<ResponseDto>> GetUserOrders()
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _responseDto.Errormessage = "Please login";
                return Unauthorized(_responseDto);
            }

            var res = await _orderService.GetAllOrders(new Guid(UserId));
            _responseDto.Result = res;
            return Ok(_responseDto);


        }

        [HttpGet("{Id}")]

        public async Task<ActionResult<ResponseDto>> GetOneOrder(Guid Id)
        {

            var res = await _orderService.GetOrderById(Id);
            _responseDto.Result = res;
            return Ok(_responseDto);


        }
        [HttpPost("Pay")]

        public async Task<ActionResult<ResponseDto>> makePayments(StripeRequestDto dto)
        {

            var sR = await _orderService.MakePayments(dto);

            _responseDto.Result = sR;
            return Ok(_responseDto);
        }
    }
}
