using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;
using TextInterpreter.Responses;

namespace TextInterpreter.Interactions
{
    public class ResponseManager
    {
        ResponseBuffer Buffer;
        Responses Response;
        ResponseData Memory;
        CharlieResponses Charlie;
        public ResponseManager()
        {
            Buffer = new ResponseBuffer();
            Response = new Responses();
            Memory = new ResponseData();
            Charlie = new CharlieResponses();
        }
        public string Respond(Context input)
        {
            string response = null;
            switch (Memory.NPC)
            {
                case CommonEnums.NPC.Charlie:
                    response = RespondCharlie(input.Dialog);
                    break;
                default:
                    response = "You cannot talk to " + input.Object1.ToString();
                    break;
            }
            //Fill in logic for handling dialog here
            return response;
        }
        public void SetNPC(CommonEnums.Interactables NPC, out string error)
        {
            error = null;
            switch (NPC)
            {
                case CommonEnums.Interactables.Charlie:
                    Memory.NPC = CommonEnums.NPC.Charlie;
                    break;
                default:
                    error = "You cannot talk to that";
                    break;
            }
        }
        private string RespondCharlie(string input)
        {
            Memory.ConversationNumber = Buffer.GetLastResponse(Memory.NPC);
            string res = Charlie.NextResponse(input, Memory.ConversationNumber, out int next);
            Buffer.SetNextResponse(Memory.NPC, next);
            Memory.ConversationNumber = next;
            return res;
        }
    }
}
