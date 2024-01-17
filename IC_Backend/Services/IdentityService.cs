using IC_Backend.Models;
using IC_Backend.Options;
using IC_Backend.ResponseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IC_Backend.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly DatabaseContext databaseContext;

        public IdentityService(UserManager<Usuario> userName, JwtSettings jwtSettings, TokenValidationParameters tokenValidationParameters, DatabaseContext databaseContext)
        {
            _userManager = userName;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            this.databaseContext = databaseContext;
        }

        public async Task<AuthenticationResult> RegisterAsync(string userName, string password, string mail, string phone, string rol)
        {
            var existingUser = await _userManager.FindByNameAsync(userName);
            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "El usuario ya existe" }
                };
            }
            var newUser = new Usuario
            {
                UserName = userName,
                NormalizedUserName = userName.ToUpper(),
                Email = mail,
                NormalizedEmail = mail.ToUpper(),
                celular = phone

            };

            var createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "No se pudo registrar" }
                };
            }

            var getUser = await _userManager.FindByNameAsync(userName);
            var setRole = await _userManager.AddToRoleAsync(getUser, rol.ToUpper());

            return await GenerateAthenticationResultForUserAsync(getUser);
        }

        public async Task<AuthenticationResult> LoginAsync(string userMail, string password)
        {
            var user = await _userManager.FindByEmailAsync(userMail);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "El usuario no existe" }
                };
            }
            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "El usuario o la contraseña son incorrectos" }
                };
            }
            var getRol = await _userManager.GetRolesAsync(user);
            return await GenerateAthenticationResultForUserAsync(user);
        }



        private async Task<AuthenticationResult> GenerateAthenticationResultForUserAsync(Usuario newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var roles = await _userManager.GetRolesAsync(newUser);
            var claims = new List<Claim>();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims: new[]
                {
                    new Claim(type: JwtRegisteredClaimNames.Sub, value: newUser.Id),
                    new Claim(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
                    new Claim(type: "name", value: newUser.UserName)

                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };
            foreach (var role in roles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(type: ClaimTypes.Role, value: role));
            }

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }

        
        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                    return null;
                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase);
        }

        public async Task<bool> ChangePassword(string userId, string oldPassword, string newPassWord)
        {
            Usuario tempUser = await _userManager.FindByIdAsync(userId);
            databaseContext.Users.Update(tempUser);
            await databaseContext.SaveChangesAsync();
            var resp = await _userManager.ChangePasswordAsync(tempUser, oldPassword, newPassWord);
            if (resp.Succeeded)
                return true;
            return false;
        }
    }
}
