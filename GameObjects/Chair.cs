using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;
using TextInterpreter.Locations;

namespace TextInterpreter.GameObjects
{
    class Chair : BaseGameObject
    {
        public Chair()
        {
            Name = "chair";
            Size = CommonEnums.Size.Large;
        }
        public string Description = "An old office chair worn from being sat in for 8 hours a day.";
    }
}
