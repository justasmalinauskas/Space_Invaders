using System;
using System.Collections.Generic;
using System.Linq;
using Space_Invaders.GameParticles;

namespace Space_Invaders
{
    public class TheGame
    {
        private const int AlienShipsRow = 5;
        private const int AlienShipsColumn = 6;
        private readonly int _defenderId;
        private readonly char[,] _gameField = new char[15, 15];
        private bool _movingright = true;
        private bool _gameStatus;
        private static readonly List<Particle> Particles = new List<Particle>();

        public TheGame()
        {
            DrawGameSpace();
            _defenderId = GetDefenderId();
            _gameStatus = true;
        }


        private void DrawGameSpace()
        {
            for (var y = 0; y < _gameField.GetUpperBound(1); y++)
                for (var x = 0; x < _gameField.GetUpperBound(0); x++)
                    if (DefenderStart(y, x))
                        Particles.Add(new Defender(x, y));
            DrawAliens();
        }

        private static void DrawAliens()
        {
            for (var x = 0; x < AlienShipsRow; x++)
                for (var y = 0; y < AlienShipsColumn; y++)
                    Particles.Add(new Alien(x, y));
        }

        private bool DefenderStart(int i, int j)
        {
            return j == _gameField.GetUpperBound(1) - 2 && i == (_gameField.GetUpperBound(0) - 1) / 2;
        }

        public void RenderGameSpace()
        {
            for (var x = 0; x < _gameField.GetUpperBound(1); x++)
            {
                for (var y = 0; y < _gameField.GetUpperBound(0); y++)
                {
                    if (HasAnyParticle(x, y))
                        _gameField[y, x] = Particles.Find(t => t.X == x && t.Y == y).Skin;
                    else
                        _gameField[y, x] = ' ';
                }
            }

            for (var x = 0; x < _gameField.GetUpperBound(1); x++)
            {
                for (var y = 0; y < _gameField.GetUpperBound(0); y++)
                    Console.Write(_gameField[y, x] + "\t");
                Console.WriteLine("\t");
            }
            if (!HasGameLost()) return;
            Console.WriteLine("You lost...");
            _gameStatus = false;
        }


        private static bool HasAnyParticle(int x, int y)
        {
            return Particles.Any(t => t.X == x && t.Y == y);
        }

        private bool HasGameLost()
        {
            return Particles.Any(t => t.X == _gameField.GetUpperBound(0) - 2 && t.Type.Equals("Alien"));
        }

        public void UpdateScrren(int signalTimeMillisecond)
        {
            Console.Clear();
            MoveAliens();
            MoveShot();
            CountAliens();
            RenderGameSpace();
        }

        private static void MoveShot()
        {
            foreach (var particle in Particles)
            {
                if (!particle.Type.Equals("Projectile")) 
                    continue;
                particle.MoveUp();
                ShotDown(particle);
            }
        }

        private static void ShotDown(Particle particle)
        {
            foreach (var alien in Particles)
            {
                if (!alien.Type.Equals("Alien")) continue;
                if (particle.X == alien.X && particle.Y == alien.Y)
                    Particles.Remove(alien);
            }
        }

        private void MoveAliens()
        {
            foreach (var particle in Particles)
            {
                if (!particle.Type.Equals("Alien")) 
                    continue;
                if (_movingright)
                {   
                    if (particle.Y == _gameField.GetUpperBound(0) - 1)
                    {
                        MoveDown();
                        _movingright = false;
                    }
                    particle.Y = particle.Y + 1;                     
                }
                else
                {
                    if (particle.Y == _gameField.GetLowerBound(0) + 1)
                    {
                        MoveDown();
                        _movingright = true;
                    }
                    particle.Y = particle.Y - 1;
                }
            }
        }
            
        private static void MoveDown()
        {
            foreach (var particle in Particles)
                if (particle.Type.Equals("Alien"))
                    particle.X++;
        }
        private static void CountAliens()
        {
            var count = 0;
            foreach (var particle in Particles)
                if (particle.Type.Equals("Alien"))
                    count++;
            if (count == 0)
                DrawAliens();
        }

        public bool IsOnGoing()
        {
            return _gameStatus;
        }

        private static int GetDefenderId()
        {
            for (var i = 0; i < Particles.Count; i++)
                if (Particles[i].Type.Equals("Defender"))
                    return i;
            return new int();
        }

        public void DefenderAction(Actions action)
        {
            switch (action)
            {
                case Actions.Left:
                    if (DefenderInBounds())
                        Particles[_defenderId].Y--;
                    break;
                case Actions.Right:
                    if (DefenderInBounds())
                        Particles[_defenderId].Y++;
                    break;
                case Actions.Shot:
                    Particles.Add(new Projectile(Particles[_defenderId].X, Particles[_defenderId].Y));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }

        }

        private bool DefenderInBounds()
        {
            return (Particles[_defenderId].Y > 0 && Particles[_defenderId].Y < _gameField.GetUpperBound(0)-1);
        }
    }  
}