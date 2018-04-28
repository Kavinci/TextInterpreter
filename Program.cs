using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using TextInterpreter.Common;

namespace TextInterpreter
{
    class Program
    {
        static BaseState GameState;
        static Screen Screen;
        static GameManager GameManager;
        static void Main(string[] args)
        {
            StartSequence();
            GameLoop();
        }
        static void StartSequence()
        {
            GameState = new BaseState();
            //Set saved game state here
            GameManager = new GameManager(GameState);
            GameState.Set(CommonEnums.Controls.None);
            List<string> start = new List<string>() { "start" };
            Screen = new Screen(GameManager.Process(start));
            Screen.Render();
        }
        static void GameLoop()
        {
            if (GameState.Get == CommonEnums.Controls.None)
            {
                Screen.ReadToBuffer();
                Screen.WriteToBuffer(GameManager.Process(Screen.Input));
                if (GameState.Get == CommonEnums.Controls.Exit)
                {
                    GameState.Increment();
                    //Save state here
                }
                else if (GameState.Get == CommonEnums.Controls.Clear)
                {
                    Screen.Clear();
                    GameState.Set(CommonEnums.Controls.None);
                    GameState.Increment();
                    GameLoop();
                }
                else
                {
                    Screen.Render();
                    GameState.Increment();
                    GameLoop();
                }
            }
            Screen.Exit();
        }
    }
}
