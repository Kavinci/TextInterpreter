using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter
{
    class BaseGameObject
    {
        public string Name;
        public CommonEnums.Size Size;
        public List<CommonEnums.Interactables> Contains { get; set; }
        public CommonEnums.Interactables ObjectLocation { get; set; }
        public CommonEnums.LocationType RoomLocation { get; set; }
    }
}
