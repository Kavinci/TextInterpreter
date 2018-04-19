using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Locations;
using TextInterpreter.Common;

namespace TextInterpreter
{
    //Location object definition
    public class LocationsManager
    {
        private Office Office;
        private Hallway Hallway;

        public LocationsManager()
        {
            Office = new Office();
            Hallway = new Hallway();
        }
        //Returns the description of a location object
        public string GetDescription(CommonEnums.LocationType location)
        {
            switch (location)
            {
                case CommonEnums.LocationType.Office:
                    return Office.Description;
                case CommonEnums.LocationType.Hallway:
                    return Hallway.Description;
            }
            return "Error in Location.GetDescription";
        }
        //Returns the contents of a location object
        public List<CommonEnums.Interactables> GetContents(CommonEnums.LocationType location)
        {
            List<CommonEnums.Interactables> contents = new List<CommonEnums.Interactables>();
            switch (location)
            {
                case CommonEnums.LocationType.Office:
                    contents = Office.Contains;
                    break;
                case CommonEnums.LocationType.Hallway:
                    contents = Hallway.Contains;
                    break;
            }
            return contents;
        }
        public void AddContents(CommonEnums.LocationType currentLocation, CommonEnums.Interactables item)
        {
            foreach(CommonEnums.Interactables x in GetContents(currentLocation))
            {
                if(x != item)
                {
                    switch (currentLocation)
                    {
                        case CommonEnums.LocationType.Office:
                            Office.Contains.Add(item);
                            break;
                        case CommonEnums.LocationType.Hallway:
                            Hallway.Contains.Add(item);
                            break;
                    }
                }
            }
        }
        public void RemoveContents(CommonEnums.LocationType currentLocation, CommonEnums.Interactables item)
        {
            foreach(CommonEnums.Interactables x in GetContents(currentLocation))
            {
                if(x == item)
                {
                    switch (currentLocation)
                    {
                        case CommonEnums.LocationType.Office:
                             Office.Contains.Remove(item);
                            break;
                        case CommonEnums.LocationType.Hallway:
                            Hallway.Contains.Remove(item);
                            break;
                    }
                }
            }
            
        }
        public bool isDirection(CommonEnums.LocationType currentLocation, CommonEnums.Direction direction)
        {
            bool isPath = false;
            switch (currentLocation)
            {
                case CommonEnums.LocationType.Office:
                    isPath = Office.Pathways.ContainsKey(direction);
                    break;
                case CommonEnums.LocationType.Hallway:
                    isPath = Hallway.Pathways.ContainsKey(direction);
                    break;
            }
            return isPath;
        }
        public CommonEnums.LocationType DirectionToLocation(CommonEnums.LocationType currentLocation, CommonEnums.Direction direction)
        {
            CommonEnums.LocationType location = currentLocation;
            if(isDirection(currentLocation, direction))
            {
                switch (currentLocation)
                {
                    case CommonEnums.LocationType.Office:
                        Office.Pathways.TryGetValue(direction, out location);
                        break;
                    case CommonEnums.LocationType.Hallway:
                        Hallway.Pathways.TryGetValue(direction, out location);
                        break;
                }
            }
            return location;
        }
    }
}
