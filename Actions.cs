using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter
{
    //Action functions
    class Actions
    {
        private PlayerCharacter PC;
        private IOData Data;
        private Locations Loc;
        private GameObjects obj;
        public Actions(PlayerCharacter character, Locations locations, IOData iO, GameObjects objects)
        {
            PC = character;
            Loc = locations;
            Data = iO;
            obj = objects;
        }
        public void GetInventory()
        {
            string inventory = "";
            int i = 0;
            foreach (string x in PC.AllItems())
            {
                if (x != null)
                {
                    switch (i)
                    {
                        case 0:
                            inventory = x;
                            Data.ToWrite = "You have " + inventory + " in your inventory.";
                            Data.RenderCommand = "write";
                            i++;
                            break;
                        case 1:
                            inventory = inventory + " and " + x;
                            Data.ToWrite = "You have " + inventory + " in your inventory.";
                            Data.RenderCommand = "write";
                            i++;
                            break;
                        case 2:
                            inventory = x + "," + inventory.Replace("and", ", and");
                            Data.ToWrite = "You have " + inventory + " in your inventory.";
                            Data.RenderCommand = "write";
                            i++;
                            break;
                    }
                }
            }
            if (inventory == "")
            {
                Data.ToWrite = "You have no items in your inventory.";
                Data.RenderCommand = "write";
            }
            else
            {

            }
        }
        public void Take(string item)
        {
            PC.AddItem(item);
        }
        public void Put(string item, string location)
        {
            //remove item from inventory and place it in the location
        }
        public void Use(string item, string target)
        {
            //if item exists in inventory and target exists in location then perform action, depends on item used
        }
        public void Look(Locations.LocationType item)
        {
            if (Loc.GetContents(PC.Location).Contains(item))
            {
                Data.ToWrite = obj.GetDescription(item);
                Data.RenderCommand = "write";
            }
            else if (PC.AllItems().Contains(item))
            {
                Data.ToWrite = obj.GetDescription(item);
                Data.RenderCommand = "write";
            }
            else
            {
                Data.ToWrite = "That item is nowhere to be found.";
                Data.RenderCommand = "write";
            }
        }
        public void Go(string location)
        {
            if (location != null && location != PC.Location)
            {
                foreach (string x in Loc.Directions(PC.Location))
                {
                    if (location == x)
                    {
                        Data.ToWrite = "You went " + x + " direction";
                        Data.RenderCommand = "write";
                    }
                }
                foreach (string x in Loc.LocationObjects())
                {
                    if (location == x)
                    {
                        PC.Location = x;
                        Data.ToWrite = Loc.GetDescription(PC.Location);
                        Data.RenderCommand = "write";
                    }
                }
            }
        }
        public void Drop(string item)
        {
            PC.RemoveItem(item);
        }
    }
}
