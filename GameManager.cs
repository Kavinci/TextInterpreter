using System;
using System.Collections.Generic;
using System.Text;
using TextInterpreter.Interactions;
using TextInterpreter.Common;

namespace TextInterpreter
{
    public class GameManager
    {
        InteractionsManager InteractionsManager;
        BaseState GameState;
        public GameManager(BaseState State)
        {
            GameState = State;
            InteractionsManager = new InteractionsManager();
        }
        public string Process(List<string> input)
        {
            Context Context = new Context();
            Context = SetDialog(Context, input);
            foreach (string x in input)
            {
                Context = SetControl(Context, x);
                Context = SetObjects(Context, x);
                Context = SetLocation(Context, x);
                Context = SetDirection(Context, x);
                Context = SetAction(Context, x);
            }
            if (Context.Control == CommonEnums.Controls.Exit)
            {
                GameState.Set(CommonEnums.Controls.Exit);
                return null;
            }
            else if (Context.Control == CommonEnums.Controls.Clear)
            {
                GameState.Set(CommonEnums.Controls.Clear);
                return null;
            }
            else
            {
                return InteractionsManager.ProcessContext(Context);
            }
        }
        private Context SetDialog(Context Context, List<string> input)
        {
            string response = null;
            foreach(string x in input)
            {
                response = response + x;
            }
            Context.Dialog = response;
            return Context;
        }
        private Context SetControl(Context Context, string input)
        {
            foreach (CommonEnums.Controls y in Enum.GetValues(typeof(CommonEnums.Controls)))
            {
                if (Context.Control == CommonEnums.Controls.None)
                {
                    if (y.ToString().ToLower() == input)
                    {
                        Context.Control = y;
                        return Context;
                    }
                    else if (input == "quit")
                    {
                        Context.Control = CommonEnums.Controls.Exit;
                        return Context;
                    }
                }
            }
            return Context;
        }
        private Context SetObjects(Context Context, string input)
        {
            foreach (CommonEnums.Interactables y in Enum.GetValues(typeof(CommonEnums.Interactables)))
            {
                if (Context.Object1 == CommonEnums.Interactables.None)
                {
                    if (y.ToString().ToLower() == input)
                    {
                        Context.Object1 = y;
                        return Context;
                    }
                }
                if (Context.Object2 == CommonEnums.Interactables.None)
                {
                    if (y.ToString().ToLower() == input)
                    {
                        Context.Object2 = y;
                        return Context;
                    }
                }
            }
            return Context;
        }
        private Context SetLocation(Context Context, string input)
        {
            Context.Location = CommonEnums.LocationType.None;
            foreach (CommonEnums.LocationType y in Enum.GetValues(typeof(CommonEnums.LocationType)))
            {
                if (y.ToString().ToLower() == input)
                {
                    Context.Location = y;
                    return Context;
                }
            }
            return Context;
        }
        private Context SetDirection(Context Context, string input)
        {
            Context.Direction = CommonEnums.Direction.None;
            foreach (CommonEnums.Direction y in Enum.GetValues(typeof(CommonEnums.Direction)))
            {
                if (y.ToString().ToLower() == input)
                {
                    Context.Direction = y;
                    return Context;
                }
            }
            return Context;
        }
        private Context SetAction(Context Context, string input)
        {
            foreach (CommonEnums.Actions y in Enum.GetValues(typeof(CommonEnums.Actions)))
            {
                if (Context.Action == CommonEnums.Actions.None)
                {
                    if (y.ToString().ToLower() == input)
                    {
                        Context.Action = y;
                        return Context;
                    }
                }
            }
            // if no action or control given, assume dialog with NPC
            if (Context.Control == CommonEnums.Controls.None && Context.Action == CommonEnums.Actions.None)
            {
                Context.Action = CommonEnums.Actions.Dialog;
            }
            return Context;
        }
    }
}
