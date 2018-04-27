using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.Locations
{
    class Hallway : BaseLocationObject
    {
        public Hallway()
        {
            Contains = new List<CommonEnums.Interactables>();
            Pathways = new Dictionary<CommonEnums.Direction, CommonEnums.LocationType>();
            Pathways.Add(CommonEnums.Direction.East, CommonEnums.LocationType.Office);
            Pathways.Add(CommonEnums.Direction.E, CommonEnums.LocationType.Office);
        }
        public string Description = "It's a hallway. It runs North to South and seems to lead to nowhere." + CommonValues.NewLine +
            "Behind you to the East is a door with the name Charlie Chaplon in vinyl lettering.";
    }
}
