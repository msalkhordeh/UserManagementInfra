using UM.Sdk.Enum;
using UM.Sdk.Model.Identity.User.Request;
using UM.Sdk.Model.Identity.User.Response;

namespace UM.ServiceProvider.Service.Identity
{
    public interface IUserService
    {
        Task<int> AddAsync(AddRequest request,
           CancellationToken cancellationToken = default);

        Task<UpdateResponse> UpdateAsync(UpdateRequest request,
            CancellationToken cancellationToken = default);

        Task<RequestResult> DeleteAsync(int userId,
            CancellationToken cancellationToken = default);

        Task<GetAllResponse> GetAllAsync(
            CancellationToken cancellationToken = default);
    }
}
