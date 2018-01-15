namespace Space_Invaders.GameParticles
{
    public class Defender : Particle
    {
        public Defender(int i, int j)
        {
            Skin = Helper.HexToChar("02C6");
            I = i;
            J = j;
            Type = "Defender";
        }
    }
}