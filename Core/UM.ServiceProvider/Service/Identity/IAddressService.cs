using UM.Sdk.Model.Identity.Address.Request;
using UM.Sdk.Model.Identity.Address.Response;

namespace UM.ServiceProvider.Service.Identity
{
    public interface IAddressService
    {
        Task<AddResponse> AddAsync(AddRequest request,
            CancellationToken cancellationToken = default);
    }
}
