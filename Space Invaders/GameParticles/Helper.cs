namespace Space_Invaders.GameParticles
{
    public static class Helper
    {
        public static char HexToChar(string hex)
        {
            return (char)ushort.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        }
    }
}