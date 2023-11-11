namespace IC_Backend.Options
{
    public class JwtSettings
    {
        public string Secret { get; set; }

        public int TokenLifeTime { get; set; }
    }
}
