using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EncounterAPI.Models;

namespace EncounterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly EncounterContext _context;

        public UsersController(EncounterContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/username
        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUser(string username, string password)
        {
            var user = await _context.Users.Where(u => u.Username == username).SingleAsync();

            if (user == null)
            {
                return NotFound();
            }

            var hashedPassword = Convert.FromBase64String(user.Password);
            if(PasswordHasher.ComparePasswords(hashedPassword, password) != 0)
            {
                return Forbid();
            }

            return user;
        }

        // PUT: api/Users/username
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{userId}")]
        public async Task<IActionResult> PutUser(long userId, User user)
        {
            if (userId != user.ID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var hashedPassword = PasswordHasher.HashPassword(user.Password);
            user.Password = Convert.ToBase64String(hashedPassword);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { username = user.Username }, user);
        }

        private bool UserExists(long userId)
        {
            return _context.Users.Any(e => e.ID == userId);
        }
    }
}
