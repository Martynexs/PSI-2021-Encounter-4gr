using System.Collections.Generic;
using System.Linq;

namespace Encounter
{
    public class PlayerUtils 
    {
        public static List<User> GetAllPlayers()
        {
            List<User> listPlayers = new List<User>();

            foreach (var player in listPlayers)
            {
                listPlayers.Add(player);
               
            }
            return listPlayers;
        }

        public static IEnumerable<User> GetPlayersFemales()
        {
            var queryResult =  from student in GetAllPlayers()
                               where student.Gender == "female"
                               select student;
            return queryResult;
        }

        public static IEnumerable<IGrouping<string,User>> GetPlayersGroupedByNationality()
        {
            var queryResult1 = from player in GetAllPlayers()
                               group player by player.Nationality;
            return queryResult1;
        }

    }
}
