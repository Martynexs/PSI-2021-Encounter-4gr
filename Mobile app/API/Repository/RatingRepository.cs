using Contracts;
using EncounterAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RatingRepository : RepositoryBase<Rating>, IRatingRepository
    {
        public RatingRepository(EncounterContext encounterContext)
            : base(encounterContext)
        {
        }

        public void CreateRating(Rating rating)
        {
            Create(rating);
            Utilities.UpdateRouteRating(rating.RouteId, RepositoryContext);
        }

        public void DeleteRating(Rating rating)
        {
            Delete(rating);
            Utilities.UpdateRouteRating(rating.RouteId, RepositoryContext);
        }

        public async Task<IEnumerable<Rating>> GetAllRatings()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<Rating> GetRating(long routeId, long userId)
        {
            return await FindByCondition(x => x.RouteId == routeId && x.UserId == userId).FirstOrDefaultAsync();
        }

        public void UpdateRating(Rating rating)
        {
            Update(rating);
            Utilities.UpdateRouteRating(rating.RouteId, RepositoryContext);
        }
    }
}
