using IC_Backend.ResponseModels;

namespace IC_Backend.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string userName, string password, string mail, string phone, string rol);
        Task<AuthenticationResult> LoginAsync(string userMail, string password);
        Task<bool> ChangePassword(string userId, string oldPassword, string newPassword);
    }
}
