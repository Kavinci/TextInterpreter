using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.Interactions
{
    public class ResponseBuffer
    {
        private Dictionary<CommonEnums.NPC, int> NPCResponseLog { get; set; }
        public ResponseBuffer()
        {
            NPCResponseLog = new Dictionary<CommonEnums.NPC, int>();
        }
        public bool SetNextResponse(CommonEnums.NPC NPC, int responseNumber)
        {
            bool Success = false;
            if (NPCResponseLog.ContainsKey(NPC))
            {
                NPCResponseLog[NPC] = responseNumber;
                Success = true;
            }
            if(Success == false)
            {
                NPCResponseLog.Add(NPC, responseNumber);
                Success = true;
            }
            return Success;
        }
        public int GetLastResponse(CommonEnums.NPC NPC)
        {
            int Last = 000;
            if (NPCResponseLog.ContainsKey(NPC))
            {
                Last = NPCResponseLog[NPC];
            }
            return Last;
        }
    }
}
