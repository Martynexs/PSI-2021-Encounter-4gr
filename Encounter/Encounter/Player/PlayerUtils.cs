using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Encounter
{
    public class PlayerUtils 
    {
        
        public static List<Player> GetAllPlayers()
        {
            List<Player> listPlayers = new List<Player>();

            foreach (var player in listPlayers)
            {
                listPlayers.Add(player);
               
            }
            return listPlayers;
        }

        public static IEnumerable<Player> GetPlayersFemales()
        {
            var queryResult =  from student in GetAllPlayers()
                               where student.Gender == "female"
                               select student;
            return queryResult;
        }

        public static IEnumerable<IGrouping<string,Player>> GetPlayersGroupedByNationality()
        {
            var queryResult1 = from player in GetAllPlayers()
                               group player by player.Nationality;
            return queryResult1;
        }

    }
}
