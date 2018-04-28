using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.Interactions
{
    //Action functions
    public class Actions
    {
        private Player Player;
        private GameObjectsManager GameObjects;
        private LocationsManager Locations;
        public Actions()
        {
            Player = new Player(CommonEnums.LocationType.Office);
            GameObjects = new GameObjectsManager();
            Locations = new LocationsManager();
        }
        public string Start()
        {
            return Locations.GetDescription(Player.Location);
        }
        public string Take(CommonEnums.Interactables item)
        {
            GameObjects.RemoveItemFromScene(item, Player.Location, out string response);
            if (response != null)
            {
                return response;
            }
            else
            {
                Locations.RemoveContents(Player.Location, item);
                return Player.Take(item);
            }
        }
        public string Put(CommonEnums.Interactables item1, CommonEnums.Interactables item2)
        {
            GameObjects.PutItemOnObject(item1, item2, Player.Location, out string response);
            return response;
        }
        public string Use(CommonEnums.Interactables item)
        {
            //use an item in the scene
            return "You used " + item.ToString().ToLower();
        }
        public string Look(CommonEnums.Interactables item)
        {
            return GameObjects.GetDescription(item, Player.Location);
        }
        public string Look(CommonEnums.LocationType location)
        {
            if(location == CommonEnums.LocationType.None)
            {
                return Locations.GetDescription(Player.Location);
            }
            else if(location == CommonEnums.LocationType.Inventory)
            {
                return Player.PlayerInventory();
            }
            else
            {
                return Locations.GetDescription(location);
            }
        }
        public string Go(CommonEnums.LocationType location)
        {
            Player.SetLocation(location);
            return Locations.GetDescription(Player.Location);
        }
        public string Go(CommonEnums.Direction direction)
        {
            if(Locations.isDirection(Player.Location, direction))
            {
                Player.SetLocation(Locations.DirectionToLocation(Player.Location, direction));
                return Locations.GetDescription(Player.Location);
            }
            else
            {
                return "You cannot go that direction";
            } 
        }
        public string Drop(CommonEnums.Interactables item)
        {
            string response = Player.Drop(item);
            if(response == null)
            {
                GameObjects.AddItemToScene(item, Player.Location);
                Locations.AddContents(Player.Location, item);
                response = item.ToString() + " has been dropped from your inventory";
            }
            return response;
        }
    }
}
