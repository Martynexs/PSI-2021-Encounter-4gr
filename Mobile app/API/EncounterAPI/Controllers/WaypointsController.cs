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
using System.Linq;

namespace EncounterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaypointsController : ControllerBase
    {
        private readonly EncounterContext _context;

        public WaypointsController(EncounterContext context)
        {
            _context = context;
        }

        // GET: api/Waypoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WaypointDTO>>> GetWaypoints()
        {
            return await _context.Waypoints.Select(wp => wp.ToDTO()).ToListAsync();
        }

        // GET: api/Waypoints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WaypointDTO>> GetWaypoint(long id)
        {
            var waypoint = await _context.Waypoints.FindAsync(id);

            if (waypoint == null)
            {
                return NotFound();
            }

            return waypoint.ToDTO();
        }

        // PUT: api/Waypoints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWaypoint(long id, Waypoint waypoint)
        {
            if (id != waypoint.Id)
            {
                return BadRequest();
            }

            _context.Entry(waypoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        public async Task<ActionResult<Waypoint>> PostWaypoint(Waypoint waypoint)
        {
            _context.Waypoints.Add(waypoint);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWaypoint), new { id = waypoint.Id }, waypoint);
        }

        // DELETE: api/Waypoints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWaypoint(long id)
        {
            var waypoint = await _context.Waypoints.FindAsync(id);
            if (waypoint == null)
            {
                return NotFound();
            }

            _context.Waypoints.Remove(waypoint);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WaypointExists(long id)
        {
            return _context.Waypoints.Any(e => e.Id == id);
        }
    }
}
