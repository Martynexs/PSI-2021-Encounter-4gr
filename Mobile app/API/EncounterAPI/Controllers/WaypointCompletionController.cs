using Contracts;
using Entities.Data_Transfer_Objects;
using Entities.Models;
using Entities.TypeExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncounterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaypointCompletionController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IAuthorizationService _authorization;

        public WaypointCompletionController(IRepositoryWrapper repoWrapper, IAuthorizationService authorization)
        {
            _repository = repoWrapper;
            _authorization = authorization;
        }

        [HttpPost]
        public async Task<ActionResult> AddWaypointCompletion(WaypointCompletionDTO waypoint)
        {
            var routeWaypoints = await _repository.Waypoint.GetWaypointsByRoute(waypoint.RouteCompletionRouteId);
            if (!routeWaypoints.Where(x => x.Id == waypoint.WaypointId).Any())
            {
                return BadRequest();
            }
            await _repository.WaypointCompletion.CreateWaypointCompletion(waypoint.ToEFModel());
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpGet("{routeId}/{userId}")]
        public async Task<ActionResult<IEnumerable<WaypointCompletionDTO>>> GetCompletedWaypoints(long routeId, long userId)
        {
            var completedWaypoints = await _repository.WaypointCompletion.GetWaypointCompletions(routeId, userId);
            return completedWaypoints.Select(x => x.ToDTO()).ToList();
        }
    }
}
