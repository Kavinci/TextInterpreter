using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.GameObjects;
using TextInterpreter.Common;

namespace TextInterpreter
{
    public class Player : BaseCharacter
    {
        InventoryManager Inventory;
        public Player(CommonEnums.LocationType initialLocation)
        {
            Inventory = new InventoryManager();
            SetLocation(initialLocation);
        }
        public string Take(CommonEnums.Interactables item)
        {
            string response;
            Inventory.AddItemToInventory(item, out response);
            if(response == null)
            {
                response = item.ToString() + " has been added to your inventory.";
            }
            return response;
        }
        public string Put(CommonEnums.Interactables item)
        {
            string response;
            Inventory.RemoveItemFromInventory(item, out response);
            if (response == null)
            {
                response = item.ToString() + " has been added to your inventory.";
            }
            return response;
        }
    }
}
