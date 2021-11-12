using EncounterAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EncounterAPI.Controllers
{
    public class TokenController : Controller
    {
        private readonly EncounterContext _context;

        public TokenController(EncounterContext context)
        {
            _context = context;
        }

        [Route("/token")]
        [HttpPost]
        public async Task<IActionResult> Create(string username, string password)
        {
            var user = await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();

            if (user == default || !PasswordHasher.ComparePasswords(user.Password, password))
            {
                return BadRequest("Incorrect username or password");
            }

            return new ObjectResult(await GenerateToken(username));
        }

        private async Task<dynamic> GenerateToken(string username)
        {
            var user = await _context.Users.Where(u => u.Username == username).FirstAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("DogIsAMan'sBestFriend")),
                        SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            var output = new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserName = username
            };

            return output;
        }


    }
}
