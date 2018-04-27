using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;
using TextInterpreter.GameObjects;

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
        public GameObjectsManager()
        {
            Desk = new Desk();
            Note = new Note();
            Cup = new Cup();
            NPC = new NPC();
            Chair = new Chair();
            Pen = new Pen();
        }
        public string GetDescription(CommonEnums.Interactables item, CommonEnums.LocationType currentLocation)
        {
            string description = "That item is nowhere to be found";
            if(DoesObjectExistInScene(item, currentLocation))
            {
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
            }
            return description;
        }
        public string GetObjectName(CommonEnums.Interactables item)
        {
            if(CommonEnums.Interactables.NPC == item)
            {
                return "Charlie";
            }
            else
            {
                return item.ToString().ToLower();
            }
        }
        private bool DoesObjectExistInScene(CommonEnums.Interactables item, CommonEnums.LocationType location)
        {
            switch (item)
            {
                case CommonEnums.Interactables.Desk:
                    if(Desk.RoomLocation == location)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case CommonEnums.Interactables.Note:
                    if (Note.RoomLocation == location)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case CommonEnums.Interactables.Cup:
                    if (Cup.RoomLocation == location)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case CommonEnums.Interactables.NPC:
                    if (NPC.RoomLocation == location)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case CommonEnums.Interactables.Chair:
                    if (Chair.RoomLocation == location)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case CommonEnums.Interactables.Pen:
                    if (Pen.RoomLocation == location)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }
        private bool CanPickupItem(CommonEnums.Interactables item, CommonEnums.LocationType currentLocation)
        {
            bool status = false;
            int Size = 0;
            //Get size of item to carry and the location of the item
            switch (item)
            {
                case CommonEnums.Interactables.Desk:
                    Size = (int)Desk.Size;
                    break;
                case CommonEnums.Interactables.Note:
                    Size = (int)Note.Size;
                    break;
                case CommonEnums.Interactables.Cup:
                    Size = (int)Cup.Size;
                    break;
                case CommonEnums.Interactables.NPC:
                    Size = (int)NPC.Size;
                    break;
                case CommonEnums.Interactables.Chair:
                    Size = (int)Chair.Size;
                    break;
                case CommonEnums.Interactables.Pen:
                    Size = (int)Pen.Size;
                    break;
            }
            //If item is too big, deny
            if (Size < 2 && DoesObjectExistInScene(item, currentLocation)) status = true;
            return status;
        }
        private bool CanPutOnObject(CommonEnums.Interactables item, CommonEnums.Interactables destination, CommonEnums.LocationType currentLocation)
        {
            bool status = false;
            int ItemSize = 0;
            int DestinationSize = 0;
            //Get size and location of item to place
            switch (item)
            {
                case CommonEnums.Interactables.Desk:
                    ItemSize = (int)Desk.Size;
                    break;
                case CommonEnums.Interactables.Note:
                    ItemSize = (int)Note.Size;
                    break;
                case CommonEnums.Interactables.Cup:
                    ItemSize = (int)Cup.Size;
                    break;
                case CommonEnums.Interactables.NPC:
                    ItemSize = (int)NPC.Size;
                    break;
                case CommonEnums.Interactables.Chair:
                    ItemSize = (int)Chair.Size;
                    break;
                case CommonEnums.Interactables.Pen:
                    ItemSize = (int)Pen.Size;
                    break;
            }
            //Get size and location of item to place first item on
            switch (destination)
            {
                case CommonEnums.Interactables.Desk:
                    DestinationSize = (int)Desk.Size;
                    break;
                case CommonEnums.Interactables.Note:
                    DestinationSize = (int)Note.Size;
                    break;
                case CommonEnums.Interactables.Cup:
                    DestinationSize = (int)Cup.Size;
                    break;
                case CommonEnums.Interactables.NPC:
                    DestinationSize = (int)NPC.Size;
                    break;
                case CommonEnums.Interactables.Chair:
                    DestinationSize = (int)Chair.Size;
                    break;
                case CommonEnums.Interactables.Pen:
                    DestinationSize = (int)Pen.Size;
                    break;
            }
            //If the item is too large for the destination, deny
            if (ItemSize < DestinationSize && DoesObjectExistInScene(item, currentLocation) && DoesObjectExistInScene(destination, currentLocation)) status = true;

            return status;
        }
        public void PutItemOnObject(CommonEnums.Interactables item, CommonEnums.Interactables destination, CommonEnums.LocationType currentLocation, out string error)
        {
            error = "I don't know how to do that";
            if (item != CommonEnums.Interactables.None && destination != CommonEnums.Interactables.None)
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

                    error = "The " + destination.ToString().ToLower() + " now contains a " + item.ToString().ToLower();
                }
            }
            else
            {
                if(item == CommonEnums.Interactables.None && destination != CommonEnums.Interactables.None)
                {
                    error = "What would you like to put in the " + destination.ToString().ToLower() + "?";
                }
                if(item != CommonEnums.Interactables.None && destination == CommonEnums.Interactables.None)
                {
                    error = "Where do you want to place the " + item.ToString().ToLower() + "?";
                }
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
        public void AddItemToScene(CommonEnums.Interactables item, CommonEnums.LocationType currentLocation)
        {
                switch (item)
                {
                    case CommonEnums.Interactables.Desk:
                    Desk.RoomLocation = currentLocation;
                        break;
                    case CommonEnums.Interactables.Note:
                        Note.RoomLocation = currentLocation;
                        break;
                    case CommonEnums.Interactables.Cup:
                        Cup.RoomLocation = currentLocation;
                        break;
                    case CommonEnums.Interactables.NPC:
                        NPC.RoomLocation = currentLocation;
                        break;
                    case CommonEnums.Interactables.Chair:
                        Chair.RoomLocation = currentLocation;
                        break;
                    case CommonEnums.Interactables.Pen:
                        Pen.RoomLocation = currentLocation;
                        break;
                }
        }
        public void UseObject(CommonEnums.Interactables item)
        {
            //use an object logic here
        }
    }
}
