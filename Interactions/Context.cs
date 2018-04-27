using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Common;

namespace TextInterpreter.Interactions
{
    public class Context
    {
        public CommonEnums.Controls Control { get; set; }
        public CommonEnums.Actions Action { get; set; }
        public CommonEnums.LocationType Location { get; set; }
        public CommonEnums.Direction Direction { get; set; }
        public CommonEnums.Interactables Object1 { get; set; }
        public CommonEnums.Interactables Object2 { get; set; }
        public string Dialog { get; set; }
        public Context()
        {
            Control = CommonEnums.Controls.None;
            Action = CommonEnums.Actions.None;
            Location = CommonEnums.LocationType.None;
            Direction = CommonEnums.Direction.None;
            Object1 = CommonEnums.Interactables.None;
            Object2 = CommonEnums.Interactables.None;
            Dialog = null;
        }
    }
}
