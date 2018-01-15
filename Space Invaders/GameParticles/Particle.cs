namespace Space_Invaders.GameParticles
{
    public class Particle
    {
        public int I { get; set; }
        public int J { get; set; }
        public char Skin { get; protected set; }
        public string Type { get; protected set; }

        public virtual void MoveUp()
        {
            I--;
        }
        
        public virtual void MoveDown()
        {
            I++;
        }
        
        public virtual void MoveRight()
        {
            J++;
        }
        
        public virtual void MoveLeft()
        {
            J--;
        }
    }
}