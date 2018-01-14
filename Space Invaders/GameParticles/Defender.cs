namespace Space_Invaders.GameParticles
{
    public class Defender : Particle
    {
        public Defender(int x, int y)
        {
            Skin = Helper.HexToChar("02C6");
            X = x;
            Y = y;
            Type = "Defender";
        }
    }
}