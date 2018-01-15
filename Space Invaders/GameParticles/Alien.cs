namespace Space_Invaders.GameParticles
{
    public class Alien : Particle
    {
        public Alien(int i, int j)
        {
            Skin = Helper.HexToChar("02C7");
            I = i;
            J = j;
            Type = "Alien";
        }
    }
}