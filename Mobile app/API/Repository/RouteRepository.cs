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
    public class RouteRepository : RepositoryBase<RouteModel>, IRouteRepository
    {
        public RouteRepository(EncounterContext encounterContext)
            :base(encounterContext)
        {
        }

        public async Task<IEnumerable<RouteModel>> GetAllRoutesAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<RouteModel> GetRouteByIdAsync(long Id)
        {
            return await FindByCondition(route => route.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RouteModel>> GetUserRoutes(long Id)
        {
            return await FindByCondition(route => route.CreatorID == Id).ToListAsync();
        }

        public void CreateRoute(RouteModel route)
        {
            Create(route);
        }

        public void DeleteRoute(RouteModel route)
        {
            Delete(route);
        }

        public void UpdateRoute(RouteModel route)
        {
            Update(route);
        }
    }
}
