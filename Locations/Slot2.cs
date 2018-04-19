using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.Locations
{
    public class Slot2 : BaseInventorySlot
    {
        public Slot2(CommonEnums.Interactables item)
        {
            AddItem(item, out bool success);
        }
    }
}
