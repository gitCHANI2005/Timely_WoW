using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Repository.Repositories;
using Service.Dtos;
using Service.Interfaces;
using Service.Services;

namespace Timely.Controllers
{
    public class LoginController:ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;
        public LoginController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }
        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            var user = _userService.ValidateUser(email, password);
            if (user == null)
                return Unauthorized();

            // יצירת טוקן עם פרטי המשתמש
            var token = _jwtService.GenerateToken(user.Name, user.Role.ToString(), email);

            // החזרת הטוקן למשתמש
            return Ok(new { Token = token });
        }

        //[HttpPost("Register")]
        //public string Register([FromBody] UserDto user)
        //{
        //    string role = user.Role + "";

        //    if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(role))
        //    {
        //        return ("Email, password, and role are required.");
        //    }

        //    //if (_userService.IsEmailTaken(user.Email))
        //    //{
        //    //    return Conflict("User with this email already exists.");
        //    //}
        //    string token  = _userService.RegisterUser(user);
        //    return token;
        //}
    }
}
