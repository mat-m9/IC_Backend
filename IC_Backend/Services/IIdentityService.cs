using IC_Backend.ResponseModels;

namespace IC_Backend.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string userName, string password, string mail, string phone);
        Task<AuthenticationResult> LoginAsync(string userMail, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refresToken);
        //Task<bool> ChangePassword(string userName, string oldPassword, string newPassword);
    }
}
