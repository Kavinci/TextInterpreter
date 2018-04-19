using System;
using System.Collections.Generic;
using System.Text;

namespace TextInterpreter
{
    class BaseState
    {
        private string State { get; set; }
        private int SetCount { get; set; }
        private int LoopCount { get; set; }
        public BaseState()
        {
            SetCount = 0;
            LoopCount = 0;
            Set("start");
        }
        public void Set(string update)
        {
            State = update;
        }
        public string Get
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
