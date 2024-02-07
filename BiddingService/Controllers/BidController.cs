using AutoMapper;
using BiddingService.Models;
using BiddingService.Models.Dto;
using BiddingService.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BiddingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IArt _artservice;
        private readonly IBid _bidservice;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;
        public BidController(IArt art, IBid bid, IMapper mapper)
        {
            _artservice = art;
            _bidservice = bid;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpPost("{Id}")]
        public async Task<ActionResult<ResponseDto>> AddBid(AddBidDto addBid, string Id)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _response.Errormessage = "Please login to make a bid";
                return Unauthorized(_response);
            }
            var art = await _artservice.GetArtById(Id);
            if (string.IsNullOrWhiteSpace(art.Description))
            {
                _response.Errormessage = "Art Not Found";
                return NotFound(_response);
            }
            var bid = _mapper.Map<Bids>(addBid);
            bid.BidderId = new Guid(UserId);
            bid.ArtId = art.Id;
            bid.ArtName = art.Name;

            var res = await _bidservice.AddBid(bid);
            _response.Result = res;
            return Ok(_response);
        }
        [HttpGet("Art{Id}")]
        public async Task<ActionResult<ResponseDto>> ArtBids(string Id)
        {
            var bids = await _bidservice.GetArBids(new Guid(Id));
            _response.Result = bids;
            return Ok(_response);
        }

        [HttpGet("UserBids")]
        public async Task<ActionResult<ResponseDto>> AllMyBids()
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _response.Errormessage = "Please login to make a bid";
                return Unauthorized(_response);
            }
            var bids = await _bidservice.GetMyBids(new Guid(UserId));
            _response.Result = bids;
            return Ok(_response);
        }
        [HttpGet("HighestBidAmount")]
        public async Task<ActionResult<ResponseDto>> HighestBid()
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _response.Errormessage = "Please login to make a bid";
                return Unauthorized(_response);
            }
            var bids = await _bidservice.HighestBidsPerItem(new Guid(UserId));
            _response.Result = bids;
            return Ok(_response);
        }
        [HttpGet("MyWins")]
        public async Task<ActionResult<ResponseDto>> MyWins()
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _response.Errormessage = "Please login to make a bid";
                return Unauthorized(_response);
            }
            var bids = await _bidservice.GetMyWins(new Guid(UserId));
            _response.Result = bids;
            return Ok(_response);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ResponseDto>> OneBid(string Id)
        {
            var bids = await _bidservice.GetOneBid(new Guid(Id));
            _response.Result = bids;
            return Ok(_response);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<ResponseDto>> DeleteBid(Guid Id)
        {
            var bid = await _bidservice.GetOneBid(Id);
            if (bid == null)
            {
                _response.Errormessage = "Bid Not Found";
            }

            var res = await _bidservice.DeleteBid(bid);
            _response.Result = res;
            return Ok(_response);
        }
    }
}

