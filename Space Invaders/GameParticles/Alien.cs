namespace Space_Invaders.GameParticles
{
    public class Alien : Particle
    {
        public Alien(int x, int y)
        {
            Skin = Helper.HexToChar("02C7");
            X = x;
            Y = y;
            Type = "Alien";

        }
    }
}