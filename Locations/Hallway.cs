using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter
{
    class Hallway : BaseLocationObject
    {
        public Hallway()
        {
            Pathways.Add(CommonEnums.Direction.West, CommonEnums.LocationType.Hallway);
        }
        public string Description = "It's a hallway. It runs North to South and seems to lead to nowhere." + CommonValues.NewLine +
            "Behind you to the East is a door with the name Charlie Chaplon in vinyl lettering.";
    }
}
