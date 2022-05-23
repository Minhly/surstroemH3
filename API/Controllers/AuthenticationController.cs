using API.Dtos;
using API.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using surstroem.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public static List<LoginDto> users = new List<LoginDto>();
        private readonly IConfiguration _configuration;
        //private readonly IUserService _userService;

        public AuthenticationController(IConfiguration configuration /*IUserService userService*/)
        {
            LoginDto user = new LoginDto();
            user.Username = "Administrator";
            user.Password = "Admin123";
            user.Role = "Admin";
            users.Add(user);
            _configuration = configuration;
            //_userService = userService;
        }

        [HttpPost("RegisterUserInAPI")]
        public ActionResult<string> RegisterUser(LoginDto loginDto)
        {
            //LoginDto user = new LoginDto();
            if (loginDto.Username == null)
            {
                return BadRequest("Enter a Username");
            }
            if (loginDto.Password == null)
            {
                return BadRequest("Enter a Password");
            }
            loginDto.Role = "Normal";
            users.Add(loginDto);
            return Ok("User registered");
        }

        [HttpPost("login")]
        public ActionResult<string> Login(LoginDto request)
        {
            foreach(var user in users)
            {
                if(request.Username == user.Username && request.Password == user.Password)
                {
                    string token = CreateToken(user);
                    return Ok(token);
                }
            }

            return BadRequest("User not found.");
            /*if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }*/
        }

        private string CreateToken(LoginDto loginDto)
        {
            List<Claim> claims = new List<Claim> { };

            if (loginDto.Role == "Admin") 
            {
                claims.Add(new Claim(ClaimTypes.Name, loginDto.Username));
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Name, loginDto.Username));
                claims.Add(new Claim(ClaimTypes.DateOfBirth, DateTime.Now.ToString()));
                claims.Add(new Claim(ClaimTypes.Role, "Normal"));
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
        {
            var salt = System.Text.Encoding.UTF8.GetBytes(passwordSalt);
            using (var hmac = new HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var hash = System.Text.Encoding.UTF8.GetBytes(passwordHash);
                return computedHash.SequenceEqual(hash);
            }
        }
    }
}