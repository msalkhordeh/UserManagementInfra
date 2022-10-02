using UM.DataAccess.Entity.Identity;

namespace UM.DataAccess.Repository.Identity
{
    public interface IUserRepository
    {
        Task<User> Add(User entity,
            CancellationToken cancellationToken = default);

        Task<User?> GetById(int userId, CancellationToken cancellationToken = default);

        User Update(User user);

        void Delete(User user);

        Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken = default);

        Task<User?> GetByUsername(string username,
            CancellationToken cancellationToken = default);

        Task<User?> GetByUsernameAndPassword(string username,
            string password, CancellationToken cancellationToken = default);
    }
}
