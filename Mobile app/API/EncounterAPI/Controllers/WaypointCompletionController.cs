using Contracts;
using Contracts.Services;
using Entities.Data_Transfer_Objects;
using Entities.Models;
using Entities.TypeExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private IWaypointCompletionService _waypointCompletionService;

        public WaypointCompletionController(IRepositoryWrapper repoWrapper, IAuthorizationService authorization, IWaypointCompletionService waypointCompletionService)
        {
            _repository = repoWrapper;
            _authorization = authorization;
            _waypointCompletionService = waypointCompletionService;
        }

        [HttpPost]
        public async Task<ActionResult> AddWaypointCompletion(WaypointCompletionDTO waypoint)
        {
            var routeWaypoints = await _repository.Waypoint.GetWaypointsByRoute(waypoint.RouteCompletionRouteId);
            if (!routeWaypoints.Where(x => x.Id == waypoint.WaypointId).Any())
            {
                return BadRequest();
            }
            await _waypointCompletionService.CreateWaypointCompletion(waypoint.ToEFModel());
            return NoContent();
        }

        [HttpGet("{routeId}/{userId}")]
        public async Task<ActionResult<IEnumerable<WaypointCompletionDTO>>> GetCompletedWaypoints(long routeId, long userId)
        {
            var completedWaypoints = await _waypointCompletionService.GetWaypointCompletions(routeId, userId);
            return completedWaypoints.Select(x => x.ToDTO()).ToList();
        }
    }
}
