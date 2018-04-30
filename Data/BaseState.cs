using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter
{
    public class BaseState
    {
        private CommonEnums.Controls State { get; set; }
        private int SetCount { get; set; }
        private int LoopCount { get; set; }
        public BaseState()
        {
            SetCount = 0;
            LoopCount = 0;
            Set(CommonEnums.Controls.None);
        }
        public void Set(CommonEnums.Controls update)
        {
            State = update;
        }
        public CommonEnums.Controls Get
        {
            get { return State; }
        }
        public void Increment()
        {
            LoopCount++;
        }
        public int Loop
        {
            get { return LoopCount; }
        }
    }
}
