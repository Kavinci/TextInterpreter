using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.GameObjects
{
    class Note : BaseGameObject
    {
        public Note()
        {
            Name = "note";
            Size = CommonEnums.Size.Small;
            RoomLocation = CommonEnums.LocationType.Office;
            ObjectLocation = CommonEnums.Interactables.Desk;
            Contains = new List<CommonEnums.Interactables>();
        }
        public string Description = "Seems to be some scribbling but Charlie's hand writing is so bad you cannot make out the words.";
    }
}
