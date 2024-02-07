using ArtProductService.Models;
using ArtProductService.Models.Dto;
using ArtProductService.Services.IService;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArtProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;
        private readonly IArt _artservice;
        private readonly IUser _userservice;

        public ArtProductController(IUser userservice, IArt art, IMapper mapper)
        {
            _userservice = userservice;
            _mapper = mapper;
            _artservice = art;
            _response = new ResponseDto();
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> AddArt(AddArtDto newArt)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _response.Errormessage = "Please login to add art";
                return Unauthorized(_response);
            }

            var art = _mapper.Map<Art>(newArt);
            art.SellerId = Guid.Parse(UserId);
            var res = await _artservice.AddArt(art);
            _response.Result = res;
            return Created("", _response);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllArts()
        {
            var arts = await _artservice.GetAllArts();
            _response.Result = arts;
            return Ok(_response);
        }

        [HttpGet("myarts")]
        public async Task<ActionResult<ResponseDto>> GetMyArts()
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _response.Errormessage = "Please login to view your art";
                return Unauthorized(_response);
            }

            var arts = await _artservice.GetMyArts(Guid.Parse(UserId));
            _response.Result = arts;
            return Ok(_response);
        }
        [HttpGet("userArt{UserId}")]
        public async Task<ActionResult<ResponseDto>> UserArts(Guid UserId)
        {

            var arts = await _artservice.GetMyArts(UserId);
            _response.Result = arts;
            return Ok(_response);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ResponseDto>> GetOneArt(Guid Id)
        {
            //if(Id ==null) { }

            var art = await _artservice.GetOneArt(Id);
            _response.Result = art;
            return Ok(_response);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<ResponseDto>> EdiArt(AddArtDto eArt, Guid Id)
        {
            var art = await _artservice.GetOneArt(Id);
            if (art == null)
            {
                _response.Errormessage = "Art Not Found";
            }

            var newArt = _mapper.Map<Art>(eArt);
            var res = await _artservice.AddArt(newArt);
            _response.Result = res;
            return Created("", _response);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<ResponseDto>> DeleteArt(Guid Id)
        {
            var art = await _artservice.GetOneArt(Id);
            if (art == null)
            {
                _response.Errormessage = "Art Not Found";
            }

            var res = await _artservice.DeleteArt(art);
            _response.Result = res;
            return Ok(_response);
        }
    }
}
