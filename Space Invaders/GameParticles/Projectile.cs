namespace Space_Invaders.GameParticles
{
    public class Projectile : Particle
    {
        public Projectile(int x, int y)
        {
            Skin = Helper.HexToChar("00B7");
            X = x;
            Y = y;
            Type = "Projectile";
        }
    }
}