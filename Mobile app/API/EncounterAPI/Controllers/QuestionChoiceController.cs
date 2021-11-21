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
        public class QuestionChoiceController : ControllerBase
        {
            private readonly EncounterContext _context;

            public QuestionChoiceController(EncounterContext context)
            {
                _context = context;
            }

            // GET: api/QuestionChoices
            [HttpGet]
            public async Task<ActionResult<IEnumerable<QuestionChoiceDTO>>> GetQuestionChoices()
            {
                return await _context.QuestionChoices.Select(qc => qc.ToDTO()).ToListAsync();
            }

            // GET: api/QuestionChoices/5
            [HttpGet("{id}")]
            public async Task<ActionResult<QuestionChoiceDTO>> GetQuestionChoice(long id)
            {
            var questionChoice = await _context.QuestionChoices.FindAsync(id);

                if (questionChoice == null)
                {
                    return NotFound();
                }

                return questionChoice.ToDTO();
            }

            // PUT: api/QuestionsChoices/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
            public async Task<IActionResult> PutQuestionChoices(long id, QuestionChoiceDTO questionChoice)
            {
                if (id != questionChoice.Id)
                {
                    return BadRequest();
                }

                
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionChoiceExists(id))
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

            // POST: api/QuestionChoices
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
            public async Task<ActionResult<QuestionChoiceDTO>> PostQuestionChoice(QuestionChoiceDTO questionChoice)
            {
                var createdQuestionChoice = questionChoice.ToEFModel();
                _context.QuestionChoices.Add(createdQuestionChoice);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetQuestionChoice), new { id = createdQuestionChoice.Id }, createdQuestionChoice.ToDTO());
            }

            // DELETE: api/QuestionChoices/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteQuestionChoice(long id)
            {
                var questionChoice = await _context.QuestionChoices.FindAsync(id);
                if (questionChoice == null)
                {
                    return NotFound();
                }

                _context.QuestionChoices.Remove(questionChoice);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool QuestionChoiceExists(long id)
            {
                return _context.QuestionChoices.Any(e => e.Id == id);
            }
        }
    }



