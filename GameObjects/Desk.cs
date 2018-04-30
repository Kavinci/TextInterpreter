using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.GameObjects
{
    class Desk : BaseGameObject
    {
        public Desk()
        {
            Name = "desk";
            Size = CommonEnums.Size.Large;
            RoomLocation = CommonEnums.LocationType.Office;
            ObjectLocation = CommonEnums.Interactables.None;
            Contains = new List<CommonEnums.Interactables>();
            Contains.Add(CommonEnums.Interactables.Cup);
            Contains.Add(CommonEnums.Interactables.Note);
            Contains.Add(CommonEnums.Interactables.Pen);
        }
        public string Description()
        {
            string text = "A normal wooden desk";
            if(Contains.Count > 0)
            {
                foreach (CommonEnums.Interactables x in this.Contains)
                {
                    switch (x)
                    {
                        case CommonEnums.Interactables.Note:
                            text += "";
                            break;
                        case CommonEnums.Interactables.Cup:
                            break;
                        case CommonEnums.Interactables.Pen:
                            break;
                        case CommonEnums.Interactables.Charlie:
                        case CommonEnums.Interactables.Chair:
                            break;
                    }
                }
            }
            text += ".";
            return text;
        }
    }
}
