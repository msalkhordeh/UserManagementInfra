using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UM.DataAccess.Entity.Identity;

namespace UM.ServiceProvider.InternalService.Authentication
{
    /// <summary>
    /// Service class to provide methods those are used for JWT 
    /// </summary>
    public interface IJwtAuthenticationService
    {
        /// <summary>
        /// Create a new JWT for the given userId
        /// </summary>
        /// <param name="user">Represent the Authenticated user</param>
        /// <returns>
        /// The issued string JWT.
        /// </returns>
        string CreateTokenAuthentication(User user);

        /// <summary>
        /// Validate the given JWT is valid to continue
        /// </summary>
        /// <param name="token">Represent the JWT value</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used to trigger the cancellation of async processes.</param>
        /// <returns>
        /// A <see cref="Task"/> that represents the asynchronous save operation. The task result contains
        /// the number of state entries written to the database.
        /// </returns>
        Task<string> ValidateTokenAsync(string token, CancellationToken cancellationToken = default);

        /// <summary>
        /// Check the given JWT is expire or not
        /// </summary>
        /// <param name="token">Represent the JWT value</param>
        /// <returns>
        /// A false value that represent the given JWT is Expired, result true means token is not expired yet
        /// </returns>
        bool CheckTokenExpired(string token);
    }
}
