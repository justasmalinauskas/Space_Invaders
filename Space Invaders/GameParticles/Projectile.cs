namespace Space_Invaders.GameParticles
{
    public class Projectile : Particle
    {
        public Projectile(int i, int j)
        {
            Skin = Helper.HexToChar("00B7");
            I = i;
            J = j;
            Type = "Projectile";
        }
    }
}