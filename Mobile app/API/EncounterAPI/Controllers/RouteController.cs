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
using Contracts;

namespace EncounterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IAuthorizationService _authorization;
        private EncounterContext _context;

        public RouteController(IRepositoryWrapper repoWrapper, EncounterContext encounter, IAuthorizationService authorization)
        {
            _repository = repoWrapper;
            _context = encounter;
            _authorization = authorization;
        }

        // GET: api/Route
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteDTO>>> GetRoutes()
        {
            var routes = await _repository.Route.GetAllRoutesAsync();
            return routes.Select(r => r.ToDTO()).ToList();
        }

        // GET: api/Route/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RouteDTO>> GetRouteModel(long id)
        {
            var route = await _repository.Route.GetRouteByIdAsync(id);
            if (route == null)
            {
                return NotFound();
            }

            return Ok(route.ToDTO());
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

            _repository.Route.UpdateRoute(route.ToEFModel());


            //_context.Entry(route.ToEFModel()).State = EntityState.Modified;

            try
            {
                await _repository.SaveAsync();
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
            var routeEntity = route.ToEFModel();

            _repository.Route.CreateRoute(routeEntity);
            await _repository.SaveAsync();

            return CreatedAtAction(nameof(GetRouteModel), new { id = route.Id }, routeEntity.ToDTO());
        }

        // DELETE: api/Route/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteRouteModel(long id)
        {
            var currentUser = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;

            var routeModel = await _repository.Route.GetRouteByIdAsync(id);
            if (routeModel == null)
            {
                return NotFound();
            }

            if(routeModel.CreatorID.ToString() != currentUser)
            {
                return Forbid();
            }

            _repository.Route.DeleteRoute(routeModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RouteModelExists(long id)
        {
            return _repository.Route.FindByCondition(route => route.Id == id).Any();
        }
    }
}
