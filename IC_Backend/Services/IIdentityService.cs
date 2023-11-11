using IC_Backend.ResponseModels;

namespace IC_Backend.Services
{
    public interface IIdentityService
    {
        Task<bool> RegisterAsync(string userId, string password);
        Task<AuthenticationResult> LoginAsync(string userName, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refresToken);
        //Task<bool> ChangePassword(string userName, string oldPassword, string newPassword);
    }
}
