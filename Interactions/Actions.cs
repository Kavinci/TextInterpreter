using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.Interactions
{
    //Action functions
    public class Actions
    {
        public Actions()
        {
           
        }
        public void Take(CommonEnums.Interactables item)
        {

        }
        public void Put(CommonEnums.Interactables item)
        {
            //remove item from inventory and place it in the location
        }
        public void Use()
        {
            //if item exists in inventory and target exists in location then perform action, depends on item used
        }
        public void Look(CommonEnums.Interactables item)
        {
            
        }
        public void Look(CommonEnums.LocationType location)
        {

        }
        public void Go(string location)
        {
            
        }
        public void Drop(string item)
        {

        }
    }
}
