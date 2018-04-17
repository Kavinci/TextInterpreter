using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter
{
    class Office : BaseLocationObject
    {
        public Office()
        {
            Contains.Add(CommonEnums.Interactables.Chair);
            Contains.Add(CommonEnums.Interactables.Cup);
            Contains.Add(CommonEnums.Interactables.Desk);
            Contains.Add(CommonEnums.Interactables.NPC);
            Contains.Add(CommonEnums.Interactables.Note);
            Contains.Add(CommonEnums.Interactables.Pen);
            Pathways.Add(CommonEnums.Direction.West, CommonEnums.LocationType.Hallway);
        }
        public string Description = "You are in a small room. In front of you is a plain looking wooden desk." + CommonValues.NewLine +
                "Charlie sits at the desk. Feel free to say hello or to look around the room.";
    }
}
