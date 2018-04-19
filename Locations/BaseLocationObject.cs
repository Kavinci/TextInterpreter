using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.Locations
{
    public class BaseLocationObject
    {
        public string Name;
        public List<CommonEnums.Interactables> Contains { get; set; }
        public Dictionary<CommonEnums.Direction, CommonEnums.LocationType> Pathways { get; set; }
    }
}
