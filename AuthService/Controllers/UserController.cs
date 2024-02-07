using AuthService.Models.Dto;
using AuthService.Services.IService;
using AutoMapper;
using MailServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;
        private readonly ResponseDto _response;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;


        public UserController(IUser user, IConfiguration configuration, IMapper mapper)
        {
            _userService = user;
            _configuration = configuration;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> RegisterUser(RegisterUserDto registerUserDto)
        {
            var res = await _userService.RegisterUser(registerUserDto);

            if (string.IsNullOrWhiteSpace(res))
            {
                //this was success
                _response.Result = "User Registered Successfully";

                //add message to queue 

                var message = new UserMessageDto()
                {
                    Name = registerUserDto.Name,
                    Email = registerUserDto.Email,
                };

                var mb = new MessageBus();
                await mb.PublishMessage(message, _configuration.GetValue<string>("ServiceBus:register"));

                return Created("", _response);
            }

            _response.Errormessage = res;
            _response.IsSuccess = false;
            return BadRequest(_response);
        }


        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> loginUser(LoginRequestDto loginRequestDto)
        {
            var res = await _userService.loginUser(loginRequestDto);

            if (res.User != null)
            {
                //this was success
                _response.Result = res;
                return Created("", _response);
            }

            _response.Errormessage = "Invalid Credentials";
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ResponseDto>> GetUser(string Id)
        {
            var res = await _userService.GetUserById(Id);

            if (res == null)
            {
                _response.Errormessage = "User Not Found ";
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            //this was success
            var user = _mapper.Map<UserDto>(res);
            _response.Result = user;
            return Ok(_response);
        }


        [HttpPost("AssignRole")]
        public async Task<ActionResult<ResponseDto>> AssignRole(AssignRoleDto role)
        {
            var res = await _userService.AssignUserRoles(role.Email, role.Role);

            if (res)
            {
                //this was success
                _response.Result = res;
                return Ok(_response);
            }

            _response.Errormessage = "Error Occured ";
            _response.Result = res;
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAlUsers()
        {
            var users = await _userService.GetAllUsers();
            _response.Result = users;
            return Ok(_response);
        }
    }
}

