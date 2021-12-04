using Contracts;
using Entities.Data_Transfer_Objects;
using Entities.Models;
using Entities.TypeExtensions;
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
        public async Task<ActionResult<RouteCompletionDTO>> GetRouteCompletion(long routeId, long userId)
        {
            var routeCompletion = await _repository.RouteCompletion.GetRouteCompletion(routeId, userId);
            if(routeCompletion == default)
            {
                return NotFound();
            }
            return routeCompletion.ToDTO();
        }

        [HttpPost("{routeId}/{userId}")]
        public async Task<ActionResult<RouteCompletionDTO>> AddRouteCompletion(long routeId, long userId, RouteCompletionDTO routeCompletion)
        {
            if(routeCompletion.UserId != userId || routeCompletion.RouteId != routeId)
            {
                return BadRequest();
            }

            routeCompletion.LastVisit = DateTime.Now;

            var completion = routeCompletion.ToEFModel();

            if (RouteCompletionExists(routeId, userId))
            {
                _repository.RouteCompletion.UpdateRouteCompletion(completion);
            }
            else
            {
                _repository.RouteCompletion.CreateRouteCompletion(completion);
            }

            await _repository.SaveAsync();
            return completion.ToDTO();
        }

        [HttpGet("{routeId}/{userId}/Waypoints")]
        public async Task<ActionResult<IEnumerable<WaypointCompletionDTO>>> GetCompletedWaypoints(long routeId, long userId)
        {
            var waypoints = await _repository.WaypointCompletion.GetWaypointCompletions(routeId, userId);
            return waypoints.Select(x => x.ToDTO()).ToList();
        }

        private bool RouteCompletionExists(long routeId, long userId)
        {
            return _repository.RouteCompletion.FindByCondition(x => x.RouteId == routeId && x.UserId == userId).Any();
        }

    }
}
