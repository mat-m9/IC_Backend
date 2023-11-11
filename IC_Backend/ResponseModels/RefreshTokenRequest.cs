namespace IC_Backend.ResponseModels
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
        public string RefreshedToken { get; set; }
    }
}
