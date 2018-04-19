using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.Locations
{
    public class Slot1 : BaseInventorySlot
    {
        public Slot1(CommonEnums.Interactables item)
        {
            AddItem(item, out bool success);
        }
    }
}
