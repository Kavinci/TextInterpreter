using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;
using TextInterpreter.Locations;

namespace TextInterpreter.GameObjects
{
    class Desk : BaseGameObject
    {
        public Desk()
        {
            Name = "desk";
            Size = CommonEnums.Size.Large;
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
                        case CommonEnums.Interactables.NPC:
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
