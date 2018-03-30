using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter
{
    class PlayerCharacter
    {
        //Object constructor
        public PlayerCharacter(string item1, string item2, string item3, string startLocation)
        {
            Slot1 = item1;
            Slot2 = item2;
            Slot3 = item3;
            Location = startLocation;
        }

        //Read and write individual inventory slots
        public string Slot1 { get; set; }
        public string Slot2 { get; set; }
        public string Slot3 { get; set; }
        public string Location { get; set; }

        //Inventory count of all items
        public int ItemCount()
        {
            int count = 0;
            if (Slot1 != null)
            {
                count += 1;
            }
            if (Slot2 != null)
            {
                count += 1;
            }
            if (Slot3 != null)
            {
                count += 1;
            }
            return count;
        }

        //Bulk export of all items in inventory
        public string[] AllItems()
        {
            string[] items = { null, null, null };
            if (Slot1 != null)
            {
                items[0] = Slot1;
            }
            if (Slot2 != null)
            {
                items[1] = Slot2;
            }
            if (Slot3 != null)
            {
                items[2] = Slot3;
            }
            return items;
        }

        public void AddItem(string item)
        {
            bool itemExists = false;
            if (AllItems().Contains(item))
            {
                Data.ToWrite = "That item already exists in your inventory.";
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
                    Loc.RemoveContents(PC.Location, item);
                }
                else
                {
                    if (Slot2 == null)
                    {
                        Slot2 = item;
                        Data.ToWrite = item + " has been added to your inventory.";
                        Data.RenderCommand = "write";
                        Loc.RemoveContents(PC.Location, item);
                    }
                    else
                    {
                        if (Slot3 == null)
                        {
                            Slot3 = item;
                            Data.ToWrite = item + " has been added to your inventory.";
                            Data.RenderCommand = "write";
                            Loc.RemoveContents(PC.Location, item);
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

        public void RemoveItem(string item)
        {
            bool itemRemoved = false;
            if (AllItems().Contains(item))
            {
                if (Slot1 == item)
                {
                    Data.ToWrite = item + " has been dropped.";
                    Data.RenderCommand = "write";
                    Loc.AddContents(PC.Location, item);
                    Slot1 = null;
                    itemRemoved = true;
                }
                if (Slot2 == item)
                {
                    Data.ToWrite = item + " has been dropped.";
                    Data.RenderCommand = "write";
                    Loc.AddContents(PC.Location, item);
                    Slot2 = null;
                    itemRemoved = true;
                }
                if (Slot3 == item)
                {
                    Data.ToWrite = item + " has been dropped.";
                    Data.RenderCommand = "write";
                    Loc.AddContents(PC.Location, item);
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
