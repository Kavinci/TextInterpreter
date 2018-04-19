using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.Locations
{
    public class Slot3 : BaseInventorySlot
    {
        public Slot3(CommonEnums.Interactables item)
        {
            AddItem(item, out bool success);
        }
    }
}
