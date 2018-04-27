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
            N,
            NorthEast,
            NE,
            NorthWest,
            NW,
            South,
            S,
            SouthEast,
            SE,
            SouthWest,
            SW,
            East,
            E,
            West,
            W,
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
            Dialog,
            None
        }
        public enum Controls
        {
            Help,
            Exit,
            Clear,
            None
        }
    }
}
