using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.GameObjects
{
    public class BaseCharacter
    {
        private CommonEnums.LocationType CurrentLocation { get; set; }
        public CommonEnums.LocationType Location
        {
            get { return CurrentLocation; }
        }
        public void SetLocation(CommonEnums.LocationType location)
        {
            CurrentLocation = location;
        }
    }
}
