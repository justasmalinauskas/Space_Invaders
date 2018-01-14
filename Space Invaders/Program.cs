using System;
using System.Timers;
using Space_Invaders.GameParticles;

namespace Space_Invaders
{
    public static class Program
    {
        private static Timer _timer;
        private static TheGame _game;
        
        public static void Main(string[] args)
        {
            StartGame();
            Console.ReadLine();  
        }

        private static void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {     
            _game.UpdateScrren(e.SignalTime.Millisecond);
        }

        private static void StartGame()
        {
            _game = new TheGame();
            _timer = new Timer(1000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Interval = 500;
            _game.RenderGameSpace();
            while (_game.IsOnGoing())
            {
                _timer.Enabled = true;
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
            _timer.Enabled = false;
        }
    }
}