using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter
{
    public class GameObjectsManager
    {
        //Game objects definition and data
        private Cup Cup;
        private Chair Chair;
        private NPC NPC;
        private Note Note;
        private Pen Pen;
        private Desk Desk;
        public GameObjectsManager(List<CommonEnums.Interactables> GameObjects)
        {
            foreach(CommonEnums.Interactables x in GameObjects)
            {
                switch (x)
                {
                    case CommonEnums.Interactables.Desk:
                        Desk = new Desk();
                        break;
                    case CommonEnums.Interactables.Note:
                        Note = new Note();
                        break;
                    case CommonEnums.Interactables.Cup:
                        Cup = new Cup();
                        break;
                    case CommonEnums.Interactables.NPC:
                        NPC = new NPC();
                        break;
                    case CommonEnums.Interactables.Chair:
                        Chair = new Chair();
                        break;
                    case CommonEnums.Interactables.Pen:
                        Pen = new Pen();
                        break;
                }
            }
        }
        public string GetDescription(CommonEnums.Interactables item)
        {
            string description = "";
            switch (item)
            {
                case CommonEnums.Interactables.Desk:
                    description = Desk.Description();
                    break;
                case CommonEnums.Interactables.Note:
                    description = Note.Description;
                    break;
                case CommonEnums.Interactables.Cup:
                    description = Cup.Description;
                    break;
                case CommonEnums.Interactables.NPC:
                    description = NPC.Description;
                    break;
                case CommonEnums.Interactables.Chair:
                    description = Chair.Description;
                    break;
                case CommonEnums.Interactables.Pen:
                    description = Pen.Description;
                    break;
            }
            return description;
        }
        public string GetObjectName(CommonEnums.Interactables item)
        {
            string name = "";
                switch (item)
                {
                    case CommonEnums.Interactables.Desk:
                        name = Desk.Name;
                        break;
                    case CommonEnums.Interactables.Note:
                        name = Note.Name;
                        break;
                    case CommonEnums.Interactables.Cup:
                        name = Cup.Name;
                        break;
                    case CommonEnums.Interactables.NPC:
                        name = NPC.Name;
                        break;
                    case CommonEnums.Interactables.Chair:
                        name = Chair.Name;
                        break;
                    case CommonEnums.Interactables.Pen:
                        name = Pen.Name;
                        break;
                }
            return name;
        }
        private bool CanPickupItem(CommonEnums.Interactables item, CommonEnums.LocationType currentLocation)
        {
            bool status = false;
            int Size = 0;
            CommonEnums.LocationType itemLocation = CommonEnums.LocationType.None;
            //Get size of item to carry and the location of the item
            switch (item)
            {
                case CommonEnums.Interactables.Desk:
                    Size = (int)Desk.Size;
                    itemLocation = Desk.RoomLocation;
                    break;
                case CommonEnums.Interactables.Note:
                    Size = (int)Note.Size;
                    itemLocation = Note.RoomLocation;
                    break;
                case CommonEnums.Interactables.Cup:
                    Size = (int)Cup.Size;
                    itemLocation = Cup.RoomLocation;
                    break;
                case CommonEnums.Interactables.NPC:
                    Size = (int)NPC.Size;
                    itemLocation = NPC.RoomLocation;
                    break;
                case CommonEnums.Interactables.Chair:
                    Size = (int)Chair.Size;
                    itemLocation = Chair.RoomLocation;
                    break;
                case CommonEnums.Interactables.Pen:
                    Size = (int)Pen.Size;
                    itemLocation = Pen.RoomLocation;
                    break;
            }
            //If item is too big, deny
            if (Size < 2 && itemLocation == currentLocation) status = true;
            return status;
        }
        private bool CanPutOnObject(CommonEnums.Interactables item, CommonEnums.Interactables destination, CommonEnums.LocationType currentLocation)
        {
            bool status = false;
            int ItemSize = 0;
            int DestinationSize = 0;
            CommonEnums.LocationType itemLocation = CommonEnums.LocationType.None;
            CommonEnums.LocationType destinationLocation = CommonEnums.LocationType.None;
            //Get size and location of item to place
            switch (item)
            {
                case CommonEnums.Interactables.Desk:
                    ItemSize = (int)Desk.Size;
                    itemLocation = Desk.RoomLocation;
                    break;
                case CommonEnums.Interactables.Note:
                    ItemSize = (int)Note.Size;
                    itemLocation = Note.RoomLocation;
                    break;
                case CommonEnums.Interactables.Cup:
                    ItemSize = (int)Cup.Size;
                    itemLocation = Cup.RoomLocation;
                    break;
                case CommonEnums.Interactables.NPC:
                    ItemSize = (int)NPC.Size;
                    itemLocation = NPC.RoomLocation;
                    break;
                case CommonEnums.Interactables.Chair:
                    ItemSize = (int)Chair.Size;
                    itemLocation = Chair.RoomLocation;
                    break;
                case CommonEnums.Interactables.Pen:
                    ItemSize = (int)Pen.Size;
                    itemLocation = Pen.RoomLocation;
                    break;
            }
            //Get size and location of item to place first item on
            switch (destination)
            {
                case CommonEnums.Interactables.Desk:
                    DestinationSize = (int)Desk.Size;
                    destinationLocation = Desk.RoomLocation;
                    break;
                case CommonEnums.Interactables.Note:
                    DestinationSize = (int)Note.Size;
                    destinationLocation = Note.RoomLocation;
                    break;
                case CommonEnums.Interactables.Cup:
                    DestinationSize = (int)Cup.Size;
                    destinationLocation = Cup.RoomLocation;
                    break;
                case CommonEnums.Interactables.NPC:
                    DestinationSize = (int)NPC.Size;
                    destinationLocation = NPC.RoomLocation;
                    break;
                case CommonEnums.Interactables.Chair:
                    DestinationSize = (int)Chair.Size;
                    destinationLocation = Chair.RoomLocation;
                    break;
                case CommonEnums.Interactables.Pen:
                    DestinationSize = (int)Pen.Size;
                    destinationLocation = Pen.RoomLocation;
                    break;
            }
            //If the item is too large for the destination, deny
            if (ItemSize < DestinationSize && itemLocation == CommonEnums.LocationType.Inventory && destinationLocation == currentLocation) status = true;

            return status;
        }
        public void PutItemOnObject(CommonEnums.Interactables item, CommonEnums.Interactables destination, CommonEnums.LocationType currentLocation, out string error)
        {
            error = "This item is too large for that action.";
            bool canPut = CanPutOnObject(item, destination, currentLocation);
            if (canPut)
            {
                switch (item)
                {
                    case CommonEnums.Interactables.Desk:
                        Desk.ObjectLocation = destination;
                        Desk.RoomLocation = currentLocation;
                        break;
                    case CommonEnums.Interactables.Note:
                        Note.ObjectLocation = destination;
                        Note.RoomLocation = currentLocation;
                        break;
                    case CommonEnums.Interactables.Cup:
                        Cup.ObjectLocation = destination;
                        Cup.RoomLocation = currentLocation;
                        break;
                    case CommonEnums.Interactables.NPC:
                        NPC.ObjectLocation = destination;
                        NPC.RoomLocation = currentLocation;
                        break;
                    case CommonEnums.Interactables.Chair:
                        Chair.ObjectLocation = destination;
                        Chair.RoomLocation = currentLocation;
                        break;
                    case CommonEnums.Interactables.Pen:
                        Pen.ObjectLocation = destination;
                        Pen.RoomLocation = currentLocation;
                        break;
                }

                switch (destination)
                {
                    case CommonEnums.Interactables.Desk:
                        Desk.Contains.Add(item);
                        break;
                    case CommonEnums.Interactables.Note:
                        Note.Contains.Add(item);
                        break;
                    case CommonEnums.Interactables.Cup:
                        Cup.Contains.Add(item);
                        break;
                    case CommonEnums.Interactables.NPC:
                        NPC.Contains.Add(item);
                        break;
                    case CommonEnums.Interactables.Chair:
                        Chair.Contains.Add(item);
                        break;
                    case CommonEnums.Interactables.Pen:
                        Pen.Contains.Add(item);
                        break;
                }

                error = null;
            }
        }
        public void RemoveItemFromScene(CommonEnums.Interactables item, CommonEnums.LocationType currentLocation, out string error)
        {
            error = "The item you are looking for is not there.";
            bool canRemove = CanPickupItem(item, currentLocation);
            if (canRemove)
            {
                CommonEnums.Interactables nestedObject = CommonEnums.Interactables.None;
                switch (item)
                {
                    case CommonEnums.Interactables.Desk:
                        Desk.RoomLocation = CommonEnums.LocationType.Inventory;
                        nestedObject = Desk.ObjectLocation;
                        Desk.ObjectLocation = CommonEnums.Interactables.None;
                        break;
                    case CommonEnums.Interactables.Note:
                        Note.RoomLocation = CommonEnums.LocationType.Inventory;
                        nestedObject = Note.ObjectLocation;
                        Note.ObjectLocation = CommonEnums.Interactables.None;
                        break;
                    case CommonEnums.Interactables.Cup:
                        Cup.RoomLocation = CommonEnums.LocationType.Inventory;
                        nestedObject = Cup.ObjectLocation;
                        Cup.ObjectLocation = CommonEnums.Interactables.None;
                        break;
                    case CommonEnums.Interactables.NPC:
                        NPC.RoomLocation = CommonEnums.LocationType.Inventory;
                        nestedObject = NPC.ObjectLocation;
                        NPC.ObjectLocation = CommonEnums.Interactables.None;
                        break;
                    case CommonEnums.Interactables.Chair:
                        Chair.RoomLocation = CommonEnums.LocationType.Inventory;
                        nestedObject = Chair.ObjectLocation;
                        Chair.ObjectLocation = CommonEnums.Interactables.None;
                        break;
                    case CommonEnums.Interactables.Pen:
                        Pen.RoomLocation = CommonEnums.LocationType.Inventory;
                        nestedObject = Pen.ObjectLocation;
                        Pen.ObjectLocation = CommonEnums.Interactables.None;
                        break;
                }
                switch (nestedObject)
                {
                    case CommonEnums.Interactables.Desk:
                        Desk.Contains.Remove(item);
                        break;
                    case CommonEnums.Interactables.Note:
                        Note.Contains.Remove(item);
                        break;
                    case CommonEnums.Interactables.Cup:
                        Cup.Contains.Remove(item);
                        break;
                    case CommonEnums.Interactables.NPC:
                        NPC.Contains.Remove(item);
                        break;
                    case CommonEnums.Interactables.Chair:
                        Chair.Contains.Remove(item);
                        break;
                    case CommonEnums.Interactables.Pen:
                        Pen.Contains.Remove(item);
                        break;
                }
                error = null;
            }
        }
    }
}
