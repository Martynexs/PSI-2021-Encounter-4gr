using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EncounterAPI.Models;
using EncounterAPI.Data_Transfer_Objects;
using EncounterAPI.TypeExtensions;
using Contracts;
using Contracts.Services;

namespace EncounterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IRatingsService _ratingsService;

        public RatingsController(IRepositoryWrapper repositoryWrapper, IRatingsService ratingsService)
        {
            _repository = repositoryWrapper;
            _ratingsService = ratingsService;
        }

        // GET: api/Ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingDTO>>> GetRatings()
        {
            //var ratings = await _repository.Rating.GetAllRatings();
            var ratings = await _ratingsService.GetAllRatings();
            return ratings.Select(r => r.ToDTO()).ToList();
        }

        // GET: api/Ratings/5/3
        [HttpGet("{RouteId}/{UserId}")]
        public async Task<ActionResult<RatingDTO>> GetRating(long RouteId, long UserId)
        {
            //var rating = await _repository.Rating.GetRating(RouteId, UserId);
            var rating = await _ratingsService.GetRating(RouteId, UserId);

            if (rating == default)
            {
                return NotFound();
            }

            return rating.ToDTO();
        }

        // PUT: api/Ratings/5/3
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{RouteId}/{UserId}")]
        public async Task<IActionResult> PutRating(long RouteId, long UserId, RatingDTO rating)
        {
            if (RouteId != rating.RouteId || UserId != rating.UserId)
            {
                return BadRequest();
            }

            var createdRating = rating.ToEFModel();

            if(RatingExists(RouteId, UserId))
            {
                // _repository.Rating.UpdateRating(createdRating);
                await _ratingsService.UpdateRating(createdRating);
            }
            else
            {
                // _repository.Rating.CreateRating(createdRating);
                await _ratingsService.CreateRating(createdRating);
            }

            /*
            try
            {
                await _repository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               throw;
            }
            catch(Exception ex)
            {
                throw;
            }
            */

            return NoContent();
        }

        private bool RatingExists(long RouteId, long UserId)
        {
            return _repository.Rating.FindByCondition(r => r.RouteId == RouteId && r.UserId == UserId).Any();
        }
    }
}
