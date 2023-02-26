using Carwash.Model;
using Carwash.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Carwash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var users = await _user.GetAll();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<User>> GetByID(int Id)
        {
            var users = await _user.GetByID(Id);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            if (user == null)
            {
                if(await _user.CheckEmailExistAsync(user.Email))
                {
                    return BadRequest();
                }
                return BadRequest(new { Message = "Email alredy exists..!" });
            }
            var add = await _user.AddUser(user);
            return Ok(new
            {
                Message = "Registration Successfull"
            });
            if(user == null)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateUser(int Id, User user)
        {
            var users = await _user.UpdateUser(Id, user);
            return CreatedAtAction(nameof(GetByID), new { id = users.ID }, users);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteUser(int Id)
        {
            await _user.DeleteUser(Id);
            return Ok();
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] Login login)
        {
            if (login == null)
            {
                return BadRequest();
            }
           var u = await _user.Login(login);
            if(u == null)
            {
                return NotFound(new { Message = "User not found" });
            }
            /*return Ok(new { Message = "Login success" })*/;
            string Token = CreateJwtToken(u);
            return Ok(new
            {
                Token,
                Message = "Login Successfull"
            });
            
        }
        private async Task<ActionResult> CheckEmailExistAsync(string Email)
        {
            var check = await _user.CheckEmailExistAsync(Email);
            return Ok();
        }
        //jwt token create
        private string CreateJwtToken(User jwt)
        {
            var jwtTokenhandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role,jwt.Role),
                    new Claim(ClaimTypes.Name, $"{jwt.FirstName} {jwt.LastName}")
                });
            var credentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256);
            var Tokendescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var token = jwtTokenhandler.CreateToken(Tokendescriptor);
            return jwtTokenhandler.WriteToken(token);

        }
    }

   
}

