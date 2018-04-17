using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter
{
    class NPC : BaseGameObject
    {
        public NPC()
        {
            Name = "charlie";
            Size = CommonEnums.Size.Large;
        }
        public string Description = "Charlie Chaplan is a well dressed man. He sits in a chair with a welcoming smile.";
    }
}
