﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EncounterAPI.TypeExtensions;
using EncounterAPI.Data_Transfer_Objects;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Contracts;
using AuthorizationService;
using Microsoft.Extensions.Logging;
using System;
using Contracts.Services;

namespace EncounterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IAuthorizationService _authorization;
        private IScoresService _scoresService;
        private readonly ILogger<RouteController> _logger;

        public RouteController(IRepositoryWrapper repoWrapper, IAuthorizationService authorization, ILogger<RouteController> logger, IScoresService scoresService)
        {
            _repository = repoWrapper;
            _authorization = authorization;
            _logger = logger;
            _scoresService = scoresService;
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
            if (route == default)
            {
                return NotFound();
            }

            return Ok(route.ToDTO());
        }

        [HttpGet("User/{id}")]
        public async Task<ActionResult<IEnumerable<RouteDTO>>> GetUserRoutes(long id)
        {
            var routes = await _repository.Route.GetUserRoutes(id);
            if (routes == null)
            {
                return NotFound();
            }

            return Ok(routes.Select(r => r.ToDTO()));
        }



        // GET: api/Route/5/Waypoints
        [HttpGet("{id}/Waypoints")]
        public async Task<ActionResult<IEnumerable<WaypointDTO>>> GetRouteWaypoints(long id)
        {
            var routeModel = await _repository.Route.GetRouteByIdAsync(id);

            if (routeModel == default)
            {
                return NotFound();
            }

            var query = await _repository.Waypoint.GetWaypointsByRoute(id);
            return query.Select(wp => wp.ToDTO()).ToList();
        }

        [HttpGet("{id}/Leaderboard")]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetRouteLeaderBoard(long id)
        {
            if(!RouteModelExists(id))
            {
                return NotFound();
            }

            var rez = await _scoresService.GetRouteLeaderboard(id);
            return rez.ToList();
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
            var routeModel = await _repository.Route.GetRouteByIdAsync(id);

            var authorizationResult = await _authorization.AuthorizeAsync(User, routeModel, Operations.Delete);

            if (routeModel == default)
            {
                return NotFound();
            }

            if(authorizationResult.Succeeded)
            {
                _repository.Route.DeleteRoute(routeModel);
                await _repository.SaveAsync();

                return NoContent();
            }
            else if(User.Identity.IsAuthenticated)
            {
                return Forbid();
            }
            else
            {
                return Challenge();
            }
        }

        private bool RouteModelExists(long id)
        {
            return _repository.Route.FindByCondition(route => route.Id == id).Any();
        }
    }
}
