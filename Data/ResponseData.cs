using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.Interactions
{
    public class ResponseData
    {
        public int ConversationNumber { get; set; }
        public CommonEnums.NPC NPC { get; set; }
        public ResponseData()
        {
            ConversationNumber = 000;
            NPC = new CommonEnums.NPC();
            NPC = CommonEnums.NPC.None;
        }
    }
}
