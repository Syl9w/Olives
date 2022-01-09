using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Olives.Data;
using Olives.Dtos;
using Olives.Helpers;
using Olives.Models;


namespace Olives.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController: Controller
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;
        public AuthController(IUserRepository repository, JwtService jwtService)
        {
            _jwtService = jwtService;
            _repository = repository;
        }
        [HttpPost("hello")]
        public IActionResult Hello(){
            return Ok("Method works");
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
            };
            

            return Created("Success",_repository.Create(user));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var user = _repository.GetByEmail(loginDto.Email);

            if(user == null)
                return BadRequest(new {message = "Invalid Credentials"}); 
            
            if(!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return BadRequest(new {message = "Invalid Credentials"}); 
            }

            var jwt = _jwtService.Generate(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions{HttpOnly = true});
            
            return Ok(new {message="success"});
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);
                int userID = int.Parse(token.Issuer);

                var user = _repository.GetById(userID);
                
                return Ok(user);
            }
            catch(Exception _)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {   
            Response.Cookies.Delete("jwt");

            return Ok(new {message="success"});
        }
    }
}
