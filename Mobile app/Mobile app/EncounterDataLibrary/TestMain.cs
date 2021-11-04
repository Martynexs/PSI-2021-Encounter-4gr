using EncounterDataLibrary.Models;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncounterDataLibrary
{
    class TestMain
    {
        static public void Main(String[] args)
        {
            ApiHelper.InitializeClient();

            var tm = new TestMothods();

            //tm.GetRoute(4).Wait();
            //tm.GetRoutes().Wait();
            //tm.GetWaypoints(4).Wait();
            //tm.CreateRoute().Wait();
            tm.UpdateRoute().Wait();
            Console.WriteLine("Finished");
        }
    }

    public class TestMothods
    {
        public async Task GetRoute(long id)
        {
            try
            {
                var rt = await EncounterProcessor.GetRoute(4);
                var x = 2 + 2;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task GetRoutes()
        {
            try
            {
                var rt = await EncounterProcessor.GetAllRoutes();
                var y = 2 + 2;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task GetWaypoints(long routeId)
        {
            try
            {
                var rt = await EncounterProcessor.GetWaypoints(routeId);
                var y = 2 + 2;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task CreateRoute()
        {
            var route = new Route
            {
                CreatorId = 1,
                Name = "Kelione"
            };

            try
            {
                var rt = await EncounterProcessor.CreateRoute(route);
                var y = 2 + 2;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task UpdateRoute()
        {
            var route = new Route
            {
                Id = 5,
                CreatorId = 1,
                Name = "Tripas",
                Location = "Vilnius"
            };

            try
            {
                await EncounterProcessor.UpdateRoute(5 ,route);
                var y = 2 + 2;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }




    }
}
