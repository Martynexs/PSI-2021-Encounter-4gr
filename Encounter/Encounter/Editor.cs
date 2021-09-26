using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encounter
{
    class Editor
    {
        public List<Waypoint> list = new List<Waypoint>();
        public void AddWayPoint()
        {
            var vp = new Waypoint();
            vp.Number = 0;
            vp.Name = null;
            vp.Coordinates = null;
            vp.Type = null;
            vp.Price = null;
            vp.OpeningHours = null;
            vp.ClosingTime = null;
            vp.Description = null;
            list.Add(vp);
        }
        public void EditWayPoint(int number, string name, string coordinates, string type, string price, string opening, string closing, string descriptions, int index)
        {
            if (index != number)
            {
                Waypoint temp = list[index - 1];
                list[index - 1] = list[number - 1];
                list[number - 1] = temp;
            }
            list[index - 1].Number = number;
            list[index - 1].Name = name;
            list[index - 1].Coordinates = coordinates;
            list[index - 1].Type = type;
            list[index - 1].Price = price;
            list[index - 1].OpeningHours = opening;
            list[index - 1].ClosingTime = closing;
            list[index - 1].Description = descriptions;
        }

        public void RemoveWayPoint(int index)
        {
            list.RemoveAt(index - 1);
        }
    }
}
