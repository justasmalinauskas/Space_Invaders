using System;
using System.Timers;
using Space_Invaders.GameParticles;

namespace Space_Invaders
{
    public static class Program
    {
        private static TheGame _game;
        
        public static void Main(string[] args)
        {
            StartGame();
            Console.ReadLine();  
        }

        private static void StartGame()
        {
            _game = new TheGame();
            _game.RenderGameSpace();
            while (_game.IsOnGoing())
            {
                //_timer.Enabled = true;
                _game.UpdateScrren();
                ConsoleKeyInfo pressKey;
                pressKey = Console.ReadKey();

                switch (pressKey.Key)
                {
                    case ConsoleKey.Spacebar:
                        _game.DefenderAction(Actions.Shot);
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        _game.DefenderAction(Actions.Left);
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        _game.DefenderAction(Actions.Right);
                        break;
                }
            }
        }
    }
}