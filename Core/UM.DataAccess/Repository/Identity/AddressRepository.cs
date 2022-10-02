using UM.DataAccess.DataContext;
using UM.DataAccess.Entity.Identity;

namespace UM.DataAccess.Repository.Identity
{
    public class AddressRepository : IAddressRepository
    {
        private readonly EfCoreContext _efCoreContext;

        public AddressRepository(EfCoreContext efCoreContext)
        {
            _efCoreContext = efCoreContext;
        }

        public async Task<Address> Add(Address entity,
            CancellationToken cancellationToken = default)
        {
            return (await _efCoreContext.AddAsync(entity, cancellationToken)).Entity;
        }
    }
}
