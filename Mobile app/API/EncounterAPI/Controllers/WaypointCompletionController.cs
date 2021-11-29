using Contracts;
using Entities.Models;
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
        public async Task<IActionResult> AddWaypointCompletion(WaypointCompletion waypoint)
        {
            var routeWaypoints = await _repository.Waypoint.GetWaypointsByRoute(waypoint.RouteCompletionRouteId);
            if(!routeWaypoints.Where(x => x.Id == waypoint.WaypointId).Any())
            {
                return BadRequest();
            }
            await _repository.WaypointCompletion.CreateWaypointCompletion(waypoint);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}
