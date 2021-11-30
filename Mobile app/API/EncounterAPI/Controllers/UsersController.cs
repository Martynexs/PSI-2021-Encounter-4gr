using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EncounterAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Contracts;
using EncounterAPI.Data_Transfer_Objects;

namespace EncounterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IAuthorizationService _authorization;

        public UsersController(IRepositoryWrapper repositoryWrapper, IAuthorizationService authorization)
        {
            _repository = repositoryWrapper;
            _authorization = authorization;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            var users = await _repository.User.GetAllUsers();
            return users.ToList();
        }

        // GET: api/Users/username
        [HttpGet("{username}")]
        public async Task<ActionResult<UserModel>> GetUser(string username)
        {
            var currentUser = User.Claims.Where(c => c.Type == ClaimTypes.Name).First().Value;

            var user = await _repository.User.GetUserByUsername(username);

            if (user == default)
            {
                return NotFound();
            }

            var authorizationResult = await _authorization.AuthorizeAsync(User, user, "UserInfoPolicy");

            if (authorizationResult.Succeeded)
            {
                return user;
            }
            else if (User.Identity.IsAuthenticated)
            {
                return Forbid();
            }
            else
            {
                return Challenge();
            }
        }

        // PUT: api/Users/username
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{userId}")]
        public async Task<IActionResult> PutUser(long userId, UserModel user)
        {
            if (userId != user.ID)
            {
                return BadRequest();
            }

            var oldUser = await _repository.User.GetUserById(userId);
            var authorizationResult = await _authorization.AuthorizeAsync(User, oldUser, "UserInfoPolicy");

            if (authorizationResult.Succeeded)
            {

                if (user.Password == null)
                {
                    user.Password = oldUser.Password;
                }
                else if (user.Password != oldUser.Password)
                {
                    var password = PasswordHasher.HashPassword(user.Password);
                    user.Password = Convert.ToBase64String(password);
                }

                _repository.User.UpdateUser(user);

                try
                {
                    await _repository.SaveAsync();
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
            else if (User.Identity.IsAuthenticated)
            {
                return Forbid();
            }
            else
            {
                return Challenge();
            }
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> PostUser(UserModel user)
        {
            var hashedPassword = PasswordHasher.HashPassword(user.Password);
            user.Password = Convert.ToBase64String(hashedPassword);

            _repository.User.CreateUser(user);
            await _repository.SaveAsync();

            return CreatedAtAction("GetUser", new { username = user.Username }, user);
        }

        [HttpGet("{username}/StartedRoutes")]
        [AllowAnonymous]
        public async Task<ActionResult<RouteDTO>> GetUserStartedRoutes(string username)
        {
            return NotFound();
        }

        private bool UserExists(long userId)
        {
            return _repository.User.GetUserById(userId) == default;
        }
    }
}
