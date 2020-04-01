using System;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuthRepository _repos;
        private readonly IConfiguration _config;

        public AuthController (IAuthRepository repos, IConfiguration config) {
            _repos = repos;
            _config = config;
        }

        [HttpPost ("Register")]

        public async Task<IActionResult> Register (UserForRegisterDto userForRegisterDto) {
            //validate request 
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower ();
            if (await _repos.UserExists (userForRegisterDto.Username)) {
                return BadRequest ("User Already Exisit");

            }
            var usertocreate = new User () {
                Username = userForRegisterDto.Username,

            };
            var CreatedUser = await _repos.Registry (usertocreate, userForRegisterDto.Password);
            return StatusCode (201);

        }

        [HttpPost ("Login")]
        public async Task<IActionResult> Login (UserForLoginDto userForLoginDto) {

            var userForRpose = await _repos.Login (userForLoginDto.Username, userForLoginDto.Password);
            if (userForRpose == null) {
                return Unauthorized ();
            }
            var claims = new [] {
                new Claim (ClaimTypes.NameIdentifier, userForRpose.Id.ToString ()),
                new Claim (ClaimTypes.Name, userForRpose.Username)

            };  
           // String secret_Encoder_Key = $"1234567890 a very long word";

            var key = Encoding.UTF8.GetBytes (_config.GetSection ("AppSetting:Token").Value);
                var cred = new SigningCredentials (new SymmetricSecurityKey (key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity (claims),
                Expires = DateTime.UtcNow.AddDays (7),
                SigningCredentials = cred

            };
            var tokenHandler = new JwtSecurityTokenHandler ();

            var token = tokenHandler.CreateToken (tokenDescriptor);

             
            return Ok (new {
                token = tokenHandler.WriteToken (token)
            });

        }

    }

}