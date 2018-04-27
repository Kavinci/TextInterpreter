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
            return "Location Manager out of Sync with locations";
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
            List<CommonEnums.Interactables> contents = GetContents(currentLocation);
            if (!contents.Contains(item))
            {
                contents.Add(item);
                switch (currentLocation)
                {
                    case CommonEnums.LocationType.Office:
                        Office.Contains.Clear();
                        Office.Contains = contents;
                        break;
                    case CommonEnums.LocationType.Hallway:
                        Hallway.Contains.Clear();
                        Hallway.Contains = contents;
                        break;
                }
            }
        }
        public void RemoveContents(CommonEnums.LocationType currentLocation, CommonEnums.Interactables item)
        {
            List<CommonEnums.Interactables> contents = GetContents(currentLocation);
            if (contents.Remove(item))
            {
                switch (currentLocation)
                {
                    case CommonEnums.LocationType.Office:
                        Office.Contains.Clear();
                        Office.Contains = contents;
                        break;
                    case CommonEnums.LocationType.Hallway:
                        Hallway.Contains.Clear();
                        Hallway.Contains = contents;
                        break;
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
