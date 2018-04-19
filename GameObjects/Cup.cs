using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;
using TextInterpreter.Locations;

namespace TextInterpreter.GameObjects
{
    class Cup : BaseGameObject
    {
        public Cup()
        {
            Name = "cup";
            Size = CommonEnums.Size.Medium;
        }
        public string Description = "Normally used for holding coffee but is currently empty.";
    }
}
