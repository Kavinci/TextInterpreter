using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

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
            Screen = new Screen();
            GameManager = new GameManager();
            GameState.Set("continue");
            Screen.Render();
        }
        static void GameLoop()
        {
            if (GameState.Get == "continue")
            {
                Screen.ReadToBuffer();
                Screen.WriteToBuffer(GameManager.Process(Screen.Input));
                if (GameManager.Exit)
                {
                    GameState.Set("Exit");
                    GameState.Increment();
                    //Save state here
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
