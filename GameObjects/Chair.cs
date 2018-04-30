using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.GameObjects
{
    class Chair : BaseGameObject
    {
        public Chair()
        {
            Name = "chair";
            Size = CommonEnums.Size.Large;
            RoomLocation = CommonEnums.LocationType.Office;
            ObjectLocation = CommonEnums.Interactables.None;
            Contains = new List<CommonEnums.Interactables>();
            Contains.Add(CommonEnums.Interactables.Charlie);
        }
        public string Description = "An old office chair worn from being sat in for 8 hours a day.";
    }
}
