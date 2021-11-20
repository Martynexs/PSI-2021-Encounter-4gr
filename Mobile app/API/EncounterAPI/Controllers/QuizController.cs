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
        public class QuizController : ControllerBase
        {
            private readonly EncounterContext _context;

            public QuizController(EncounterContext context)
            {
                _context = context;
            }

            // GET: api/Quizes
            [HttpGet]
            public async Task<ActionResult<IEnumerable<QuizDTO>>> GetQuizes()
            {
                return await _context.Quizes.Select(qz => qz.ToDTO()).ToListAsync();
            }

            // GET: api/Quizes/5
            [HttpGet("{id}")]
            public async Task<ActionResult<QuizDTO>> GetQuiz(long id)
            {
            var quiz = await _context.Quizes.FindAsync(id);

                if (quiz == null)
                {
                    return NotFound();
                }

                return quiz.ToDTO();
            }

            // PUT: api/Quizes/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
            public async Task<IActionResult> PutQuiz(long id, QuizDTO quiz)
            {
                if (id != quiz.QuizId)
                {
                    return BadRequest();
                }

                
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(id))
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

            // POST: api/Quizes
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
            public async Task<ActionResult<QuizDTO>> PostQuiz(QuizDTO quiz)
            {
                var createdQuiz = quiz.ToEFModel();
                _context.Quizes.Add(createdQuiz);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetQuiz), new { id = createdQuiz.Id }, createdQuiz.ToDTO());
            }

            // DELETE: api/Quizes/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteQuiz(long id)
            {
                var quiz = await _context.Quizes.FindAsync(id);
                if (quiz == null)
                {
                    return NotFound();
                }

                _context.Quizes.Remove(quiz);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool QuizExists(long id)
            {
                return _context.Quizes.Any(e => e.Id == id);
            }
        }
    }



