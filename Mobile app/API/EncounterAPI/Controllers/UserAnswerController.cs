using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EncounterAPI.Models;
using EncounterAPI.TypeExtensions;
using Entities.Data_Transfer_Objects;
using Entities.TypeExtensions;

namespace EncounterAPI.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class UserAnswerController : ControllerBase
        {
            private readonly EncounterContext _context;

            public UserAnswerController(EncounterContext context)
            {
                _context = context;
            }

            // GET: api/UserAnswers
            [HttpGet]
            public async Task<ActionResult<IEnumerable<UserAnswerDTO>>> GetUserAnswers()
            {
                return await _context.UserAnswers.Select(ua => ua.ToDTO()).ToListAsync();
            }

            // GET: api/UserAnswers/5
            [HttpGet("{id}")]
            public async Task<ActionResult<UserAnswerDTO>> GetUserAnswer(long id)
            {
            var userAnswer = await _context.UserAnswers.FindAsync(id);

                if (userAnswer == null)
                {
                    return NotFound();
                }

                return userAnswer.ToDTO();
            }

            // PUT: api/UserAnswers/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
            public async Task<IActionResult> PutUserAnswer(long id, UserAnswerDTO userAnswer)
            {
                if (id != userAnswer.Id)
                {
                    return BadRequest();
                }

                
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAnswerExists(id))
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

            // POST: api/UserAnswers
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
            public async Task<ActionResult<UserAnswerDTO>> PostUserAnswer(UserAnswerDTO userAnswer)
            {
                var createdUserAnswer = userAnswer.ToEFModel();
                _context.UserAnswers.Add(createdUserAnswer);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUserAnswers), new { id = createdUserAnswer.Id }, createdUserAnswer.ToDTO());
            }

            // DELETE: api/UserAnswers/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteUserAnswer(long id)
            {
                var userAnswer = await _context.UserAnswers.FindAsync(id);
                if (userAnswer == null)
                {
                    return NotFound();
                }

                _context.UserAnswers.Remove(userAnswer);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool UserAnswerExists(long id)
            {
                return _context.UserAnswers.Any(e => e.Id == id);
            }
        }
    }



