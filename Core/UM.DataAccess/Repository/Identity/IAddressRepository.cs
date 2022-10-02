using UM.DataAccess.Entity.Identity;

namespace UM.DataAccess.Repository.Identity
{
    public interface IAddressRepository
    {
        Task<Address> Add(Address entity,
            CancellationToken cancellationToken = default);
    }
}
