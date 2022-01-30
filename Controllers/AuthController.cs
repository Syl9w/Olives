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
using Olives.Services;

namespace Olives.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;
        private readonly IImageService _imageService;
        public AuthController(IUserRepository repository, JwtService jwtService, IImageService imageService)
        {
            _jwtService = jwtService;
            _repository = repository;
            _imageService = imageService;
        }

        private User Get()
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
            int userID = int.Parse(token.Issuer);

            return _repository.GetById(userID);
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


            return Created("Success", _repository.Create(user));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var user = _repository.GetByEmail(loginDto.Email);

            if (user == null)
                return BadRequest(new { message = "Invalid Credentials" });

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            var jwt = _jwtService.Generate(user.Id);

            Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });

            return Ok(new { message = "success", JWT = jwt });
        }

        [HttpGet("user")]
        public IActionResult GetUser()
        {
            try
            {
                var user = Get();

                return Ok(user);
            }
            catch (Exception _)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new { message = "success" });
        }

        [HttpPost("Upload")]
        public IActionResult Upload([FromForm] AddImageDto file)
        {
            var user = new User();
            try
            {

                user = Get();
            }
            catch (Exception _)
            {
                return Unauthorized();
            }
            _imageService.Upload(file, user);
            return Ok();
        }

        [HttpGet("getImage")]
        public IActionResult GetImage()
        {
            var user = new User();
            try
            {
                user = Get();
            }
            catch (Exception _)
            {
                return Unauthorized();
            }
            var img = _imageService.GetImage(user.Id);

            return File(img.Result.ImageData, "image/jpeg");

        }

        [HttpDelete("deleteImage")]
        public IActionResult DeleteImage()
        {
            var user = new User();
            try
            {
                user = Get();
            }
            catch (Exception _)
            {
                return Unauthorized();
            }
            _imageService.DeleteImage(user);
            return Ok();
        }

        [HttpPost("addInterest")]
        public ActionResult<List<Interest>> AddInterest(AddInterestDto newInterest)
        {
            var user = new User();
            try
            {
                user = Get();
            }
            catch (Exception _)
            {
                return Unauthorized();
            }
            var newUserInterest = new AddUserInterestDto{
                UserId = user.Id,
                InterestName = newInterest.Name
            };
            var interests = _repository.AddUserInterest(newUserInterest);
           
            return Ok(interests);
        }

        [HttpGet("getInterests")]
        public ActionResult<List<Interest>> GetInterests()
        {
            var user = new User();
            try
            {
                user = Get();
            }
            catch (Exception _)
            {
                return Unauthorized();
            }
            return Ok(_repository.GetInterests(user));
        }
        [HttpGet("getSuitableFriends")]
        public ActionResult<List<User>> GetSuitableFriends(){
            var user = new User();
            try
            {
                user = Get();
            }
            catch (Exception _)
            {
                return Unauthorized();
            }
            var pf = _repository.FindSuitableFriends(user);
            return Ok(new{ pf});
        }
    }
}
