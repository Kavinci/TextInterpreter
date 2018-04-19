using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.Locations
{
    public class BaseInventorySlot
    {
        private CommonEnums.Interactables Slot { get; set; }
        public void AddItem(CommonEnums.Interactables item, out bool success)
        {
            success = false;
            if(Slot == CommonEnums.Interactables.None)
            {
                Slot = item;
                success = true;
            }
        }
        public void RemoveItem(CommonEnums.Interactables item, out bool success)
        {
            success = false;
            if(Slot == item)
            {
                Slot = CommonEnums.Interactables.None;
                success = true;
            }
        }
        public CommonEnums.Interactables Contains
        {
            get { return Slot; }
        }
    }
}
