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
        private readonly char[,] _gameField = new char[20, 30];
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
            for (var i = 0; i < _gameField.GetUpperBound(1); i++)
                for (var j = 0; j < _gameField.GetUpperBound(0); j++)
                    if (DefenderStart(i, j))
                        Particles.Add(new Defender(i, j));
            DrawAliens();
        }

        private static void DrawAliens()
        {
            for (var i = 0; i < AlienShipsRow; i++)
                for (var j = 0; j < AlienShipsColumn; j++)
                    Particles.Add(new Alien(i, j));
        }

        private bool DefenderStart(int i, int j)
        {
            return i == _gameField.GetUpperBound(1) - 2 && j == (_gameField.GetUpperBound(0) - 1) / 2;
        }

        public void RenderGameSpace()
        {
            for (var i = 0; i < _gameField.GetUpperBound(1); i++)
            {
                for (var j = 0; j < _gameField.GetUpperBound(0); j++)
                {
                    if (HasAnyParticle(i, j))
                        _gameField[j, i] = Particles.Find(t => t.I == i && t.J == j).Skin;
                    else
                        _gameField[j, i] = ' ';
                }
            }

            for (var i = 0; i < _gameField.GetUpperBound(1); i++)
            {
                for (var j = 0; j < _gameField.GetUpperBound(0); j++)
                    Console.Write(_gameField[j, i] + "\t");
                Console.WriteLine("\t");
            }
            if (!HasGameLost()) return;
            Console.WriteLine("You lost...");
            _gameStatus = false;
        }


        private static bool HasAnyParticle(int x, int y)
        {
            return Particles.Any(t => t.I == x && t.J == y);
        }

        private bool HasGameLost()
        {
            return Particles.Any(t => t.I == _gameField.GetUpperBound(1) - 3 && t.Type.Equals("Alien"));
        }

        public void UpdateScrren()
        {
            Console.Clear();
            MoveAliens();
            MoveShot();
            CountAliens();
            RenderGameSpace();
        }

        private static void MoveShot()
        {
            for (var i = 0; i < Particles.Count; i++)
            {
                if (!Particles[i].Type.Equals("Projectile"))
                    continue;
                Particles[i].MoveUp();
                ShotDown(Particles[i]);
            }
        }

        private static void ShotDown(Particle particle)
        {
            for (var i = 0; i < Particles.Count; i++)
            {
                if (!Particles[i].Type.Equals("Alien")) continue;
                if (particle.I != Particles[i].I || particle.J != Particles[i].J) continue;
                Particles.RemoveAt(i);
                for (var j = 0; j < Particles.Count; j++)
                {
                    if (!Particles[j].Type.Equals("Projectile") || particle.I != Particles[j].I || particle.J != Particles[j].J) continue;
                    Particles.RemoveAt(j);
                }
            }   
        }

        //Nepermeta normaliai per eilute, kai priarteja prie krasto
        private void MoveAliens()
        {
            foreach (var particle in Particles)
            {
                if (!particle.Type.Equals("Alien")) 
                    continue;
                if (_movingright)
                {   
                    particle.MoveRight();   
                    if (particle.J == _gameField.GetUpperBound(0) - 1)
                    {
                        MoveAliensOnX(Actions.Right, particle.I);
                        MoveDown();
                        _movingright = false;
                        break;
                    }
                                      
                }
                else
                {
                    particle.MoveLeft();
                    if (particle.J == _gameField.GetLowerBound(0) + 1)
                    {
                        MoveAliensOnX(Actions.Left, particle.I);
                        MoveDown();
                        _movingright = true;
                        break;
                    }
                    
                }
            }
        }

        private static void MoveAliensOnX(Actions action, int I)
        {
            switch (action)
            {
                case Actions.Right:
                {
                    foreach (var particle in Particles)
                        if (particle.Type.Equals("Alien") && particle.I == I-2)
                            particle.MoveRight();

                    break;
                }
                case Actions.Left:
                {
                    foreach (var particle in Particles)
                        if (particle.Type.Equals("Alien") && particle.I == I+2)
                            particle.MoveLeft();

                    break;
                }
            }
        }
            
        private static void MoveDown()
        {
            foreach (var particle in Particles)
                if (particle.Type.Equals("Alien"))
                    particle.MoveDown();
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
                    if (DefenderInBounds(Actions.Left))
                        Particles[_defenderId].MoveLeft();
                    break;
                case Actions.Right:
                    if (DefenderInBounds(Actions.Right))
                        Particles[_defenderId].MoveRight();
                    break;
                case Actions.Shot:
                    Particles.Add(new Projectile(Particles[_defenderId].I, Particles[_defenderId].J));
                    break;
                default: 
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }

        }

        private bool DefenderInBounds(Actions action)
        {
            if (action == Actions.Left)
                return (Particles[_defenderId].J > 0 && Particles[_defenderId].J < _gameField.GetUpperBound(0) - 1);
            return (Particles[_defenderId].J >= 0 && Particles[_defenderId].J < _gameField.GetUpperBound(0) - 2);
        }
    }  
}