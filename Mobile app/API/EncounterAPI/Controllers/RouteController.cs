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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EncounterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly EncounterContext _context;

        public RouteController(EncounterContext context)
        {
            _context = context;
        }

        // GET: api/Route
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteDTO>>> GetRoutes()
        {
            return await _context.Routes.Select(rt => rt.ToDTO()).ToListAsync();
        }

        // GET: api/Route/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RouteDTO>> GetRouteModel(long id)
        {
            var routeModel = await _context.Routes.FindAsync(id);

            if (routeModel == null)
            {
                return NotFound();
            }

            return routeModel.ToDTO();
        }

        // GET: api/Route/5/Waypoints
        [HttpGet("{id}/Waypoints")]
        public async Task<ActionResult<IEnumerable<WaypointDTO>>> GetRouteWaypoints(long id)
        {
            var routeModel = await _context.Routes.FindAsync(id);

            if (routeModel == null)
            {
                return NotFound();
            }

            var query = await _context.Waypoints.Where(wp => wp.RouteId == id).Select(wp => wp.ToDTO()).ToListAsync();
            return query;
        }


        // PUT: api/Route/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRouteModel(long id, RouteDTO route)
        {
            if (id != route.Id)
            {
                return BadRequest();
            }

            _context.Entry(route.ToEFModel()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteModelExists(id))
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

        // POST: api/Route
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RouteDTO>> PostRouteModel(RouteDTO route)
        {
            var createdRoute = route.ToEFModel();
            _context.Routes.Add(createdRoute);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRouteModel), new { id = route.Id }, createdRoute.ToDTO());
        }

        // DELETE: api/Route/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRouteModel(long id)
        {
            var currentUser = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;

            var routeModel = await _context.Routes.FindAsync(id);
            if (routeModel == null)
            {
                return NotFound();
            }

            if(routeModel.CreatorID.ToString() != currentUser)
            {
                return Forbid();
            }

            _context.Routes.Remove(routeModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RouteModelExists(long id)
        {
            return _context.Routes.Any(e => e.Id == id);
        }
    }
}
