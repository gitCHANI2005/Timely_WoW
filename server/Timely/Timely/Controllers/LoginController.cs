using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Repository.Repositories;
using Service.Dtos;
using Service.Interfaces;
using Service.Services;

namespace Timely.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;
        public LoginController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public string Login([FromBody] UserDto user)
        {
            var _user = _userService.ValidateUser(user.Email, user.Password);
            if (_user == null)
                return "user can not be null";
            string token = _jwtService.GenerateToken(_user.Name, _user.Role.ToString(), _user.Email);

            return token;
        }
    }
}
