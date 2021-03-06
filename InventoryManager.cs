﻿using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;
using TextInterpreter.Locations;

namespace TextInterpreter
{
    public class InventoryManager
    {
        Slot1 Slot1;
        Slot2 Slot2;
        Slot3 Slot3;
        private Dictionary<CommonEnums.InventorySlot,CommonEnums.Interactables> InventorySlots { get; set; }
        public InventoryManager()
        {
            Slot1 = new Slot1(CommonEnums.Interactables.None);
            Slot2 = new Slot2(CommonEnums.Interactables.None);
            Slot3 = new Slot3(CommonEnums.Interactables.None);
            InventorySlots = new Dictionary<CommonEnums.InventorySlot, CommonEnums.Interactables>();
            InventorySlots.Add(CommonEnums.InventorySlot.Slot1, Slot1.Contains);
            InventorySlots.Add(CommonEnums.InventorySlot.Slot2, Slot2.Contains);
            InventorySlots.Add(CommonEnums.InventorySlot.Slot3, Slot3.Contains);
        }
        private void RefreshInventory()
        {
            InventorySlots.Remove(CommonEnums.InventorySlot.Slot1);
            InventorySlots.Remove(CommonEnums.InventorySlot.Slot2);
            InventorySlots.Remove(CommonEnums.InventorySlot.Slot3);
            InventorySlots.Add(CommonEnums.InventorySlot.Slot1, Slot1.Contains);
            InventorySlots.Add(CommonEnums.InventorySlot.Slot2, Slot2.Contains);
            InventorySlots.Add(CommonEnums.InventorySlot.Slot3, Slot3.Contains);
        }
        public void AddItemToInventory(CommonEnums.Interactables item, out string error)
        {
            error = "Inventory is full";
            bool success = false;
            if (success == !true)
            {
                Slot1.AddItem(item, out success);
                error = null;
            }
            if (success == !true)
            {
                Slot2.AddItem(item, out success);
                error = null;
            }
            if (success == !true)
            {
                Slot3.AddItem(item, out success);
                error = null;
            }
            RefreshInventory();
        }
        public void RemoveItemFromInventory(CommonEnums.Interactables item, out string error)
        {
            error = "That item is not in your inventory";
            bool success = false;
            if (success == !true)
            {
                Slot1.RemoveItem(item, out success);
                error = null;
            }
            if (success == !true)
            {
                Slot2.RemoveItem(item, out success);
                error = null;
            }
            if (success == !true)
            {
                Slot3.RemoveItem(item, out success);
                error = null;
            }
            RefreshInventory();
        }
        public string GetInventory()
        {
            string Inventory = "You currently have ";
            List<CommonEnums.Interactables> ItemsHeld = new List<CommonEnums.Interactables>();
            foreach(CommonEnums.Interactables x in InventorySlots.Values)
            {
                if(x != CommonEnums.Interactables.None)
                {
                    ItemsHeld.Add(x);
                }
            }
            if(ItemsHeld.Count == 0)
            {
                return "Your inventory is empty";
            }
            else
            {
                switch (ItemsHeld.Count)
                {
                    case 1:
                        Inventory = Inventory + " a " + ItemsHeld[0].ToString().ToLower();
                        break;
                    case 2:
                        Inventory = Inventory + ItemsHeld[0].ToString().ToLower() + " and " + ItemsHeld[1].ToString().ToLower();
                        break;
                    case 3:
                        Inventory = Inventory + ItemsHeld[0].ToString().ToLower() + ", " + ItemsHeld[1].ToString().ToLower() + ", and " + ItemsHeld[2].ToString().ToLower();
                        break;
                }
                return Inventory + " in your inventory";
            }
        }
    }
}
