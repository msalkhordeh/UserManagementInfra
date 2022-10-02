using Microsoft.EntityFrameworkCore;
using UM.DataAccess.DataContext;
using UM.DataAccess.Entity.Identity;
using UM.Utility;

namespace UM.DataAccess.Repository.Identity
{
    public class UserRepository : IUserRepository
    {
        private readonly EfCoreContext _efCoreContext;

        public UserRepository(EfCoreContext efCoreContext)
        {
            _efCoreContext = efCoreContext;
        }

        public async Task<User> Add(User entity,
            CancellationToken cancellationToken = default)
        {
            var user = (await _efCoreContext.AddAsync(entity,
                cancellationToken)).Entity;
            return user;
        }

        public async Task<User?> GetById(int userId,
            CancellationToken cancellationToken = default)
        {
            return await _efCoreContext.Users
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }

        public User Update(User user)
        {
            return _efCoreContext.Update(user).Entity;
        }

        public void Delete(User user)
        {
            _efCoreContext.Remove(user);
        }

        public async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _efCoreContext.Users
                .ToListAsync(cancellationToken);
        }

        public async Task<User?> GetByUsername(string username,
            CancellationToken cancellationToken = default)
        {
            return await _efCoreContext.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username),
                cancellationToken);
        }

        public async Task<User?> GetByUsernameAndPassword(string username, string password, CancellationToken cancellationToken = default)
        {
           var user = await _efCoreContext.Users.FirstOrDefaultAsync(u =>
           u.Username.ToLower().Equals(username),cancellationToken);

            if(user != null)
            {
                var result = password.ValidateHash(user.PasswordSalt,
                    user.PasswordHash);
                if (result)
                {
                    return user;
                }
                else
                {
                    return default;
                }
            }
            return default;
        }
    }
}
