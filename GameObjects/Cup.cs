using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.GameObjects
{
    class Cup : BaseGameObject
    {
        public Cup()
        {
            Name = "cup";
            Size = CommonEnums.Size.Medium;
            RoomLocation = CommonEnums.LocationType.Office;
            ObjectLocation = CommonEnums.Interactables.Desk;
            Contains = new List<CommonEnums.Interactables>();
        }
        public string Description = "Normally used for holding coffee but is currently empty.";
    }
}
