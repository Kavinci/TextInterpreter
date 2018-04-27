using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.GameObjects
{
    class NPC : BaseGameObject
    {
        public NPC()
        {
            Name = "charlie";
            Size = CommonEnums.Size.Large;
            RoomLocation = CommonEnums.LocationType.Office;
            ObjectLocation = CommonEnums.Interactables.Chair;
            Contains = new List<CommonEnums.Interactables>();
        }
        public string Description = "Charlie Chaplan is a well dressed man. He sits in a chair with a welcoming smile.";
    }
}
