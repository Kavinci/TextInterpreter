using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.GameObjects
{
    class Pen : BaseGameObject
    {
        public Pen()
        {
            Name = "pen";
            Size = CommonEnums.Size.Small;
            RoomLocation = CommonEnums.LocationType.Office;
            ObjectLocation = CommonEnums.Interactables.Desk;
            Contains = new List<CommonEnums.Interactables>();
        }
        public string Description = "A pen of blue ink, good for writing Notes.";
    }
}
