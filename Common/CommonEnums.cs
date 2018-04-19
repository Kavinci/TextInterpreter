using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter.Common
{
    public class CommonEnums
    {
        public enum Interactables
        {
            None,
            Cup,
            Chair,
            NPC,
            Desk,
            Note,
            Pen
        }
        public enum Size
        {
            Small,
            Medium,
            Large
        }
        public enum LocationType
        {
            None,
            Inventory,
            Office,
            Hallway
        }
        public enum Direction
        {
            North,
            NorthEast,
            NorthWest,
            South,
            SouthEast,
            SouthWest,
            East,
            West,
            None
        }
        public enum InventorySlot
        {
            Slot1,
            Slot2,
            Slot3
        }
        public enum Actions
        {
            Take,
            Put,
            Hit,
            Use,
            Throw,
            Look,
            Go,
            Drop,
            Help,
            None
        };
    }
}
