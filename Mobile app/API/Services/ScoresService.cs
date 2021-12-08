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
    public class ScoresService : IScoresService
    {
        private EncounterContext _repository;
        public ScoresService(EncounterContext context)
        {
            _repository = context;
        }

        public async Task<IEnumerable<dynamic>> GetRouteLeaderboard(long routeId)
        {
            var scores = await  _repository.RouteCompletions.Join(_repository.Users,
                                                                  cpl => cpl.UserId,
                                                                  u => u.ID,
                                                                  (cpl, u) => new
                                                                  {
                                                                      Route = cpl.RouteId,
                                                                      User = u.Username,
                                                                      Score = cpl.Points
                                                                  })
                .Where(x => x.Route == routeId)
                .Select(x => new { x.User, x.Score })
                .ToListAsync();

            return scores;
        }
    }
}
