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
    public class RatingsController : ControllerBase
    {
        private readonly EncounterContext _context;

        public RatingsController(EncounterContext context)
        {
            _context = context;
        }

        // GET: api/Ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRatings()
        {
            return await _context.Ratings.ToListAsync();
        }

        // GET: api/Ratings/5/3
        [HttpGet("{RouteId}/{UserId}")]
        public async Task<ActionResult<Rating>> GetRating(long RouteId, long UserId)
        {
            var rating = await _context.Ratings.FindAsync(RouteId, UserId);

            if (rating == null)
            {
                return NotFound();
            }

            return rating;
        }

        // PUT: api/Ratings/5/3
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{RouteId}/{UserId}")]
        public async Task<IActionResult> PutRating(long RouteId, long UserId, Rating rating)
        {
            if (RouteId != rating.RouteId || UserId != rating.UserId)
            {
                return BadRequest();
            }


            if(RatingExists(RouteId, UserId))
            {
               _context.Entry(rating).State = EntityState.Modified;
            }
            else
            {
                _context.Ratings.Add(rating);
            }

            RatingsLogic.UpdateRating(rating, _context);

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
