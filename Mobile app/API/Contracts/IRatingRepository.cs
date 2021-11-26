using EncounterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRatingRepository : IRepositoryBase<Rating>
    {
        Task<IEnumerable<Rating>> GetAllRatings();
        Task<Rating> GetRating(long routeId, long userId);
        void CreateRating(Rating rating);
        void UpdateRating(Rating rating);
        void DeleteRating(Rating rating);
    }
}
