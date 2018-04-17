using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter
{
    class Note : BaseGameObject
    {
        public Note()
        {
            Name = "note";
            Size = CommonEnums.Size.Small;
        }
        public string Description = "Seems to be some scribbling but Charlie's hand writing is so bad you cannot make out the words.";
    }
}
