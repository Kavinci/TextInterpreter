using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter
{
    class PlayerCharacter
    {
        Inventory Inventory;
        public CommonEnums.LocationType CurrentLocation { get; set; }
        public PlayerCharacter(CommonEnums.Interactables item1, CommonEnums.Interactables item2, CommonEnums.Interactables item3)
        {
            Inventory = new Inventory(item1, item2, item3);
        }
        

        public void RemoveItem(string item)
        {
            bool itemRemoved = false;
            foreach (string thing in AllItems())
            {
                if (thing == item)

                {
                    if (Slot1 == item)
                    {
                        Data.ToWrite = item + " has been dropped.";
                        Data.RenderCommand = "write";
                        Loc.AddContents(Location, item);
                        Slot1 = null;
                        itemRemoved = true;
                    }
                    if (Slot2 == item)
                    {
                        Data.ToWrite = item + " has been dropped.";
                        Data.RenderCommand = "write";
                        Loc.AddContents(Location, item);
                        Slot2 = null;
                        itemRemoved = true;
                    }
                    if (Slot3 == item)
                    {
                        Data.ToWrite = item + " has been dropped.";
                        Data.RenderCommand = "write";
                        Loc.AddContents(Location, item);
                        Slot3 = null;
                        itemRemoved = true;
                    }
                }
                if (!itemRemoved)
                {
                    Data.ToWrite = "You are not carrying that item.";
                    Data.RenderCommand = "write";
                }
            }
        }
    }
}
