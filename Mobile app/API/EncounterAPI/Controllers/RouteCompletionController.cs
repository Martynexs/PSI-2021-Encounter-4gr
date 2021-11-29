using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncounterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteCompletionController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IAuthorizationService _authorization;

        public RouteCompletionController(IRepositoryWrapper repoWrapper, IAuthorizationService authorization)
        {
            _repository = repoWrapper;
            _authorization = authorization;
        }

        [HttpGet("{routeId}/{userId}")]
        public async Task<ActionResult<RouteCompletion>> GetRouteCompletion(long routeId, long userId)
        {
            var routeCompletion = await _repository.RouteCompletion.GetRouteCompletion(routeId, userId);
            if(routeCompletion == default)
            {
                return NotFound();
            }
            return routeCompletion;
        }

        [HttpPost("{routeId}/{userId}")]
        public async Task<ActionResult<RouteCompletion>> AddRouteCompletion(long routeId, long userId, RouteCompletion routeCompletion)
        {
            if(routeCompletion.UserId != userId || routeCompletion.RouteId != routeId)
            {
                return BadRequest();
            }

            routeCompletion.LastVisit = DateTime.Now;

            if (RouteCompletionExists(routeId, userId))
            {
                _repository.RouteCompletion.UpdateRouteCompletion(routeCompletion);
            }
            else
            {
                _repository.RouteCompletion.CreateRouteCompletion(routeCompletion);
            }

            await _repository.SaveAsync();
            return routeCompletion;
        }

        [HttpGet("{routeId}/{userId}/Waypoints")]
        public async Task<ActionResult<IEnumerable<WaypointCompletion>>> GetCompletedWaypoints(long routeId, long userId)
        {
            var waypoints = await _repository.WaypointCompletion.GetWaypointCompletions(routeId, userId);
            return waypoints.ToList();
        }

        private bool RouteCompletionExists(long routeId, long userId)
        {
            return _repository.RouteCompletion.FindByCondition(x => x.RouteId == routeId && x.UserId == userId).Any();
        }

    }
}
