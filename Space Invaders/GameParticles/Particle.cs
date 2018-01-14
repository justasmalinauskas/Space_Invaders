namespace Space_Invaders.GameParticles
{
    public class Particle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Skin { get; protected set; }
        public string Type { get; protected set; }

        public void MoveUp()
        {
            X--;
        }
    }
}