using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;
using TextInterpreter.Locations;

namespace TextInterpreter.GameObjects
{
    class Pen : BaseGameObject
    {
        public Pen()
        {
            Name = "pen";
            Size = CommonEnums.Size.Small;
        }
        public string Description = "A pen of blue ink, good for writing Notes.";
    }
}
