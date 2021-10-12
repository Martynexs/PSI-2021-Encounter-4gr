using Encounter.Models;
using System.Collections.Generic;
using System.Linq;

namespace Encounter
{
    public class HypotheticalGroupJoinExample
    {
        private readonly List<Route> _routes;
        private readonly List<User> _users;

        public HypotheticalGroupJoinExample(List<Route> routes, List<User> users)
        {
            _routes = routes;
            _users = users;
        }

        public IEnumerable<Route> GetUserRoutes(int userID)
        {
            var groupJoin =
                _users.GroupJoin(_routes,
                                user => user.ID,
                                route => route.UserID,
                                (user, routes) => new
                                {
                                    Creator = user,
                                    Routes = routes
                                });

            var query = from item in groupJoin
                        where item.Creator.ID == userID
                        select item.Routes;

            return query.Single();
        }
    }
}
