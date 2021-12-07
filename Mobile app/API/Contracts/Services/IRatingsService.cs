using EncounterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface IRatingsService
    {
        Task CreateRating(Rating rating);
        Task DeleteRating(Rating rating);
        Task<IEnumerable<Rating>> GetAllRatings();
        Task<Rating> GetRating(long routeId, long userId);
        Task UpdateRating(Rating rating);
    }
}
