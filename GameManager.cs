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
        Player Player;
        public bool Exit { get; set; }
        public GameManager()
        {
            Exit = false;
            InteractionsManager = new InteractionsManager();
            Player = new Player(CommonEnums.LocationType.Office);
        }
        public string Process(List<string> input)
        {
            Context Context = new Context();
            foreach (string x in input)
            {
                Context = SetObjects(Context, x);
                Context = SetLocation(Context, x);
                Context = SetDirection(Context, x);
                Context = SetAction(Context, x);
            }
            return InteractionsManager.ProcessContext(Context);
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
                    }
                }
                if(Context.Object2 == CommonEnums.Interactables.None)
                {
                    if (y.ToString().ToLower() == input)
                    {
                        Context.Object2 = y;
                    }
                }
            }
            return Context;
        }
        private Context SetLocation(Context Context, string input)
        {
            foreach (CommonEnums.LocationType y in Enum.GetValues(typeof(CommonEnums.LocationType)))
            {
                if (Context.Location == CommonEnums.LocationType.None)
                {
                    if (y.ToString().ToLower() == input)
                    {
                        Context.Location = y;
                    }
                }
            }
            return Context;
        }
        private Context SetDirection(Context Context, string input)
        {
            foreach (CommonEnums.Direction y in Enum.GetValues(typeof(CommonEnums.Direction)))
            {
                if (Context.Direction == CommonEnums.Direction.None)
                {
                    if (y.ToString().ToLower() == input)
                    {
                        Context.Direction = y;
                    }
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
                    }
                }
            }
            return Context;
        }
    }
}
