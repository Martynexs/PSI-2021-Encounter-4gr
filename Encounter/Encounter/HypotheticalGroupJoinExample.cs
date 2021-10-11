using Encounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter
{
    class HypotheticalGroupJoinExample
    {
        List<Route> Routes;
        List<User> Users;

        public HypotheticalGroupJoinExample(List<Route> routes, List<User> users)
        {
            Routes = routes;
            Users = users;
        }

        public IEnumerable<Route> GetUserRoutes(int userID)
        {
            var groupJoin =
                Users.GroupJoin(Routes,
                                user => user.ID,
                                route => route.UserID,
                                (user, routes) => new
                                {
                                    Creator = user,
                                    Routes = routes
                                });

            var query = from i in groupJoin
                   where i.Creator.ID == userID
                   select i.Routes;

            return query.Single();
        }

    }
}
