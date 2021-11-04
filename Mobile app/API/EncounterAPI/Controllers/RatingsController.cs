using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EncounterAPI.Models;
using EncounterAPI.TypeExtensions;
using EncounterAPI.Data_Transfer_Objects;

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
            return await _context.Ratings.Select(r => r.ToDTO()).ToListAsync();
        }

        // GET: api/Ratings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RatingDTO>> GetRating(long id)
        {
            var rating = await _context.Ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return rating.ToDTO();
        }

        // PUT: api/Ratings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(long id, Rating rating)
        {
            if (id != rating.Id)
            {
                return BadRequest();
            }

            _context.Entry(rating).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                UpdateRating(rating);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
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

        // POST: api/Ratings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RatingDTO>> PostRating(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            UpdateRating(rating);

            return CreatedAtAction(nameof(GetRating), new { id = rating.Id }, rating.ToDTO());
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(long id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();

            UpdateRating(rating);

            return NoContent();
        }

        private bool RatingExists(long id)
        {
            return _context.Ratings.Any(e => e.Id == id);
        }

        private async void UpdateRating(Rating rating)
        {
            var route = _context.Routes.Find(rating.RouteId);
            var query = (from rt in _context.Ratings
                         where rt.RouteId == route.Id
                         group rt by rt.RouteId into grp
                         select new
                         {
                             rowCount = grp.Count(),
                             rowSum = grp.Sum(x => x.Value)
                         }).Single();

            route.Raters = query.rowCount;
            route.RateSum = query.rowSum;
            route.Rating = (double)query.rowSum / query.rowCount;

            await _context.SaveChangesAsync();
        }

    }
}
