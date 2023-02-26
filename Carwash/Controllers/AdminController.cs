using Carwash.Model;
using Carwash.Repository;
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
using Microsoft.AspNetCore.Authorization;

namespace Carwash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _admin;
        public AdminController(IAdmin admin)
        {
            _admin = admin;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var admins = await _admin.GetAll();
            if (admins == null)
            {
                return NotFound();
            }
            return Ok(admins);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Admin>> GetByID(int Id)
        {
            var admins = await _admin.GetByID(Id);
            if (admins == null)
            {
                return NotFound();
            }
            return Ok(admins);
        }
        [HttpPost]
        public async Task<ActionResult<Admin>> Add(Admin admin)
        {


            var add = await _admin.Add(admin);
            return Ok(new
            {
                Message = "Successfull"
            });
            if (admin == null)
            {
                return BadRequest();
            }
        }
        [HttpPost("AdminLogin")]
        public async Task<ActionResult> AdminLogin([FromBody] AdminLogin alogin)
        {
            if(alogin == null)
            {
                return BadRequest();
            }
            var a = await _admin.AdminLogin(alogin);
            if (a == null)
            {
                return NotFound(new { Message = "Admin not found" });
            }
            string Token = CreateJwtToken(a);
            return Ok(new
            {
                Token,
                Message = "Admin Login Successfull"
            });
        }
        //create jwt
        private string CreateJwtToken(Admin jwt)
        {
            var jwtTokenhandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, jwt.Email)

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

        [HttpPut]
        public async Task<ActionResult> Update(int Id, Admin admin)
        {
            var admins = await _admin.Update(Id, admin);
            return CreatedAtAction(nameof(GetByID), new { id = admins.Id }, admins);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id)
        {
            await _admin.Delete(Id);
            return Ok();
        }
        private async Task<ActionResult> CheckEmailExistAsync(string Email)
        {
            var check = await _admin.CheckEmailExistAsync(Email);
            return Ok();
        }

    }
   
   
    
    
    
}


