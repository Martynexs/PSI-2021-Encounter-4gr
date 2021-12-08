using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EncounterAPI.Models;
using EncounterAPI.TypeExtensions;
using EncounterAPI.Data_Transfer_Objects;
using Contracts;
using Entities.Data_Transfer_Objects;
using Entities.TypeExtensions;
using Entities.Models;

namespace EncounterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaypointsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;

        public WaypointsController(IRepositoryWrapper repositoryWrapper)
        {
            _repository = repositoryWrapper;
        }

        // GET: api/Waypoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WaypointDTO>>> GetWaypoints()
        {
            var waypoints = await _repository.Waypoint.GetAllWaypoints();
            return waypoints.Select(wp => wp.ToDTO()).ToList();
        }

        // GET: api/Waypoints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WaypointDTO>> GetWaypoint(long id)
        {
            var waypoint = await _repository.Waypoint.GetWaypointById(id);

            if (waypoint == null)
            {
                return NotFound();
            }

            return waypoint.ToDTO();
        }

        // PUT: api/Waypoints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWaypoint(long id, WaypointDTO waypoint)
        {
            if (id != waypoint.Id)
            {
                return BadRequest();
            }

            _repository.Waypoint.UpdateWaypoint(waypoint.ToEFModel());

            try
            {
                await _repository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WaypointExists(id))
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

        // POST: api/Waypoints
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WaypointDTO>> PostWaypoint(WaypointDTO waypoint)
        {
            var createdWaypoint = waypoint.ToEFModel();
            _repository.Waypoint.CreateWaypoint(createdWaypoint);
            await _repository.SaveAsync();

            return CreatedAtAction(nameof(GetWaypoint), new { id = createdWaypoint.Id }, createdWaypoint.ToDTO());
        }

        // DELETE: api/Waypoints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWaypoint(long id)
        {
            var waypoint = await _repository.Waypoint.GetWaypointById(id);
            if (waypoint == null)
            {
                return NotFound();
            }

            _repository.Waypoint.DeleteWaypoint(waypoint);
            await _repository.SaveAsync();

            return NoContent();
        }

        private bool WaypointExists(long id)
        {
            return _repository.Waypoint.GetWaypointById(id) == default;
        }

        [HttpGet("{id}/Quiz")]
        public async Task<ActionResult<QuizDTO>> GetQuizByWaypoint(long id)
        {
            var quiz = await _repository.Quiz.GetQuizByWaypointId(id);
            if (quiz == default)
            {
                return NotFound();
            }
            var answers = await _repository.QuizAnswer.GetQuizAnswers(quiz.Id);
            var result = quiz.ToDTO();
            result.Answers = answers.Select(x => x.ToDTO()).ToList();
            return result;
        }

        [HttpGet("{id}/QuizQuestions")]
        public async Task<ActionResult<List<QuizDTO>>> GetMultipleQuestionsByWaypoint(long id)
        {
            IEnumerable<Quiz> questions = await _repository.Quiz.GetMultipleQuestionsByWaypointId(id);
            if (questions == null)
            {
                return NotFound();
            }

            List<QuizDTO> result = new();
            foreach (Quiz q in questions)
            {
                QuizDTO qd = q.ToDTO();
                IEnumerable<QuizAnswers> answers = await _repository.QuizAnswer.GetQuizAnswers(q.Id);
                qd.Answers = answers.Select(x => x.ToDTO()).ToList();
                result.Add(qd);
            }
            return result;
        }

    }
}
