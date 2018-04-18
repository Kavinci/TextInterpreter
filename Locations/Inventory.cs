using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter
{
    class Inventory
    {
        private Dictionary<CommonEnums.InventorySlot,CommonEnums.Interactables> InventorySlots { get; set; }
        private CommonEnums.Interactables Slot1 { get; set; }
        private CommonEnums.Interactables Slot2 { get; set; }
        private CommonEnums.Interactables Slot3 { get; set; }
        public Inventory(CommonEnums.Interactables item1, CommonEnums.Interactables item2, CommonEnums.Interactables item3)
        {
            Slot1 = item1;
            Slot2 = item2;
            Slot3 = item3;
        }
        public int ItemCount()
        {
            RefreshInventory();
            return InventorySlots.Count;
        }
        public void RefreshInventory()
        {
            InventorySlots.Remove(CommonEnums.InventorySlot.Slot1);
            InventorySlots.Remove(CommonEnums.InventorySlot.Slot2);
            InventorySlots.Remove(CommonEnums.InventorySlot.Slot3);
            if (Slot1 != CommonEnums.Interactables.None)
            {
                InventorySlots.Add(CommonEnums.InventorySlot.Slot1 , Slot1);
            }
            if (Slot2 != CommonEnums.Interactables.None)
            {
                InventorySlots.Add(CommonEnums.InventorySlot.Slot2, Slot2);
            }
            if (Slot3 != CommonEnums.Interactables.None)
            {
                InventorySlots.Add(CommonEnums.InventorySlot.Slot3, Slot3);
            }
        }
        public void AddItem(CommonEnums.Interactables item)
        {
            foreach (CommonEnums.Interactables thing in InventorySlots.Values)
            {
                if (thing == item)
                {
                    IOData. ToWrite = "That item already exists in your inventory.";
                    Data.RenderCommand = "write";
                    itemExists = true;
                }
                if (!itemExists)
                {
                    if (Slot1 == null)
                    {
                        Slot1 = item;
                        Data.ToWrite = item + " has been added to your inventory.";
                        Data.RenderCommand = "write";
                        Loc.RemoveContents(Location, item);
                    }
                    else
                    {
                        if (Slot2 == null)
                        {
                            Slot2 = item;
                            Data.ToWrite = item + " has been added to your inventory.";
                            Data.RenderCommand = "write";
                            Loc.RemoveContents(Location, item);
                        }
                        else
                        {
                            if (Slot3 == null)
                            {
                                Slot3 = item;
                                Data.ToWrite = item + " has been added to your inventory.";
                                Data.RenderCommand = "write";
                                Loc.RemoveContents(Location, item);
                            }
                            else
                            {
                                Data.ToWrite = "Your inventory is full. Please drop an item to pick up this item.";
                                Data.RenderCommand = "write";
                            }
                        }
                    }
                }
            }
        }
    }
}
