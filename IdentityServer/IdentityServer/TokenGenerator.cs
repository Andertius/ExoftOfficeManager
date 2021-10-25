//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;

//using ExoftOfficeManager.IdentityServer;

//using Flagman.Core.Interfaces;
//using Flagman.Core.ResponseModels;
//using Flagman.Core.Settings;
//using Flagman.Domain.Entities;

//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;

//namespace IdentityServer
//{
//    public class TokenGenerator : ITokenGenerator
//    {
//        private const int RefreshTokenSize = 32;

//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly AuthSettings _authSettings;
//        public TokenGenerator(UserManager<ApplicationUser> userManager, IOptions<AuthSettings> authSettings)
//        {
//            _userManager = userManager;
//            _authSettings = authSettings.Value;
//        }

//        public string GenerateAccessToken(ApplicationUser user)
//        {
//            var handler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(_authSettings.KEY);


//            ClaimsIdentity identity = new ClaimsIdentity(new[] {
//                new Claim(ClaimTypes.Name, user.UserName),
//                new Claim(ClaimTypes.NameIdentifier, user.Id)
//            });

//            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
//            {
//                Issuer = _authSettings.ISSUER,
//                Audience = _authSettings.AUDIENCE,
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
//                Subject = identity,
//                Expires = DateTime.Now.AddHours(_authSettings.LIFETIME)
//            });
//            return handler.WriteToken(securityToken);
//        }

//        public string GenerateRefreshToken(ApplicationUser user)
//        {
//            var randomNumber = new byte[RefreshTokenSize];
//            using (var rng = RandomNumberGenerator.Create())
//            {
//                rng.GetBytes(randomNumber);
//                return Convert.ToBase64String(randomNumber);
//            }
//        }

//        public async Task<TokenResponseModel> RefreshAccessToken(string accessToken, string refreshToken)
//        {
//            var principal = GetPrincipalFromExpiredToken(accessToken);
//            var user = await _userManager.FindByNameAsync(principal.Identity.Name);
//            if (user == null || user.RefreshToken != refreshToken)
//            {
//                throw new SecurityTokenException("Invalid refresh token");
//            }

//            user.RefreshToken = GenerateRefreshToken(user);
//            var result = await _userManager.UpdateAsync(user);
//            if (!result.Succeeded)
//            {
//                throw new Exception("Unable to create refresh token");
//            }

//            return new TokenResponseModel
//            {
//                AccessToken = GenerateAccessToken(user),
//                RefreshToken = user.RefreshToken
//            };
//        }


//        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
//        {
//            var tokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateAudience = false,
//                ValidateIssuer = false,
//                ValidateIssuerSigningKey = true,
//                IssuerSigningKey = _authSettings.GetSymmetricSecurityKey(),
//                ValidateLifetime = false
//            };

//            var tokenHandler = new JwtSecurityTokenHandler();
//            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

//            return principal;
//        }
//    }
//}
