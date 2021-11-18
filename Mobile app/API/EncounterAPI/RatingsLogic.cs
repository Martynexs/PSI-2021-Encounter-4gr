using EncounterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncounterAPI
{
    public class RatingsLogic
    {
        public static void UpdateRating(Rating rating, EncounterContext context)
        {
            var route = context.Routes.Find(rating.RouteId);
            var query = (from rt in context.Ratings
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
