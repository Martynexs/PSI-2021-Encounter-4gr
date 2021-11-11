using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EncounterAPI.Models;
using EncounterAPI.Data_Transfer_Objects;
using EncounterAPI.TypeExtensions;

namespace EncounterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly EncounterContext _context;

        public RatingsController(EncounterContext context)
        {
            _context = context;
        }

        // GET: api/Ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingDTO>>> GetRatings()
        {
            return await _context.Ratings.Select(x => x.ToDTO()).ToListAsync();
        }

        // GET: api/Ratings/5/3
        [HttpGet("{RouteId}/{UserId}")]
        public async Task<ActionResult<RatingDTO>> GetRating(long RouteId, long UserId)
        {
            var rating = await _context.Ratings.FindAsync(RouteId, UserId);

            if (rating == null)
            {
                return NotFound();
            }

            return rating.ToDTO();
        }

        // PUT: api/Ratings/5/3
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{RouteId}/{UserId}")]
        public async Task<IActionResult> PutRating(long RouteId, long UserId, RatingDTO rating)
        {
            if (RouteId != rating.RouteId || UserId != rating.UserId)
            {
                return BadRequest();
            }

            var createdRating = rating.ToEFModel();

            if(RatingExists(RouteId, UserId))
            {
               _context.Entry(createdRating).State = EntityState.Modified;
            }
            else
            {
                _context.Ratings.Add(createdRating);
            }

            RatingsLogic.UpdateRating(createdRating, _context);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               throw;
            }

            return NoContent();
        }

        private bool RatingExists(long RouteId, long UserId)
        {
            return _context.Ratings.Any(e => e.RouteId == RouteId && e.UserId == UserId);
        }
    }
}
