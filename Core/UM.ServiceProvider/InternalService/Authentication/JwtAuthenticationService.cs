using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UM.DataAccess.Entity.Identity;
using UM.DataAccess.Repository.Identity;
using UM.Utility;
using static System.String;

namespace UM.ServiceProvider.InternalService.Authentication
{
    /// <inheritdoc/>
    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthUser _authenticatedUser;
        private readonly IUserRepository _userRepository;
        private const string JwtSecretKey = "qJ+xVlme7KAAcCjnCvEQnyOl1VdIXm/4Ws8F1CFEfk/KOK04xDaIjBYv1Qa+PgyDn7ikOBCh9PdmudLJS2LvrA==";
        
        /// <summary>
        /// Initialize an instance of <see cref="JwtAuthenticationService"/>
        /// </summary>
        public JwtAuthenticationService(IConfiguration configuration,
            IAuthUser authenticatedUser, IUserRepository userRepository)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _authenticatedUser = authenticatedUser ?? throw new ArgumentNullException(nameof(authenticatedUser));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public string CreateTokenAuthentication(User user)
        {
            try
            {
                IEnumerable <Claim> claims = new List<Claim>
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("Username", user.Username.ToString()),
                    new Claim("Firstname", user.FirstName.ToString()),
                    new Claim("Lastname", user.LastName.ToString()),
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var jwtToken = new JwtSecurityToken(
                    "localhost",
                    "localhost",
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: credentials
                );
                return new JwtSecurityTokenHandler().WriteToken(jwtToken);
            }
            catch (Exception)
            {
                return Empty;
            }
        }

        public async Task<string> ValidateTokenAsync(string token,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var param = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecretKey)),
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidIssuer = "localhost",
                    ValidAudience = "localhost",
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(token, param, out _);
                var validTo = handler.ReadToken(token).ValidTo;
                if (DateTime.UtcNow > validTo)
                {
                    return Empty;
                }

                var userId = handler.ReadJwtToken(token).Claims.FirstOrDefault(c =>
                    c.Type == "UserId")?.Value;

                _authenticatedUser.User = await _userRepository.GetById(userId.ToInt(), cancellationToken) ?? default;
                if (_authenticatedUser.User == null)
                {
                    return Empty;
                }

                return token;
            }
            catch (Exception)
            {
                return Empty;
            }
        }

        public bool CheckTokenExpired(string token)
        {
            try
            {
                var param = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Secret"])),
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidIssuer = _configuration["Token:Issuer"],
                    ValidAudience = _configuration["Token:Audience"],
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(token, param, out _);
                var validTo = handler.ReadToken(token).ValidTo;
                return DateTime.UtcNow <= validTo;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
