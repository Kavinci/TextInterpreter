using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Interactions;
using TextInterpreter.GameObjects;

namespace TextInterpreter
{
    public class InteractionsManager
    {
        private Actions Actions;
        private ResponseManager Response;
        private Help Help;
        public InteractionsManager()
        {
            Actions = new Actions();
            Help = new Help();
            Response = new ResponseManager();
        }
        public string ProcessContext(Context Context)
        {
            if(Context.Control == Common.CommonEnums.Controls.Help)
            {
                return Help.HelpMessage();
            }
            else if(Context.Action == Common.CommonEnums.Actions.Talk)
            {
                return Response.Respond(Context);
            }
            else if(Context.Action == Common.CommonEnums.Actions.Start)
            {
                return Actions.Start();
            }
            else
            {
                switch (Context.Action)
                {
                    //Hit and Throw are missing. Need logic for Put object inside other objects
                    case Common.CommonEnums.Actions.Drop:
                        return Actions.Drop(Context.Object1);
                    case Common.CommonEnums.Actions.Go:
                        if(Context.Location != Common.CommonEnums.LocationType.None)
                        {
                            return Actions.Go(Context.Location);
                        }
                        else
                        {
                            return Actions.Go(Context.Direction);
                        }
                    case Common.CommonEnums.Actions.Look:
                        if(Context.Object1 != Common.CommonEnums.Interactables.None)
                        {
                            return Actions.Look(Context.Object1);
                        }
                        else
                        {
                            return Actions.Look(Context.Location);
                        }
                    case Common.CommonEnums.Actions.Put:
                        return Actions.Put(Context.Object1, Context.Object2);
                    case Common.CommonEnums.Actions.Take:
                        return Actions.Take(Context.Object1);
                    case Common.CommonEnums.Actions.Use:
                        return Actions.Use(Context.Object1);
                    default:
                        return "I don't know how to do that";
                }
             
            }
        }
    }
}
