using Contracts.Services;
using EncounterAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RatingsService : IRatingsService
    {
        private EncounterContext _repository;
        public RatingsService(EncounterContext context)
        {
            _repository = context;
        }

        public async Task CreateRating(Rating rating)
        {
            _repository.Ratings.Add(rating);
            await _repository.SaveChangesAsync();
            UpdateRouteRating(rating.RouteId);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteRating(Rating rating)
        {
            _repository.Ratings.Remove(rating);
            await _repository.SaveChangesAsync();
            UpdateRouteRating(rating.RouteId);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Rating>> GetAllRatings()
        {
            return await _repository.Ratings.ToListAsync();
        }

        public async Task<Rating> GetRating(long routeId, long userId)
        {
            return await _repository.Ratings.Where(x => x.RouteId == routeId && x.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task UpdateRating(Rating rating)
        {
            _repository.Ratings.Update(rating);
            await _repository.SaveChangesAsync();
            UpdateRouteRating(rating.RouteId);
            await _repository.SaveChangesAsync();
        }

        private void UpdateRouteRating(long routeId)
        {
            var route = _repository.Routes.Find(routeId);
            var query = (from rt in _repository.Ratings
                         where rt.RouteId == route.Id
                         group rt by rt.RouteId into grp
                         select new
                         {
                             rowCount = grp.Count(),
                             rowSum = grp.Sum(x => x.Value)
                         }).First();

            route.Raters = query.rowCount;
            route.RateSum = query.rowSum;
            route.Rating = (double)query.rowSum / query.rowCount;
        }
    }
}
