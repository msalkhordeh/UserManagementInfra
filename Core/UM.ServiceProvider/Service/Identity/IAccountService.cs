using UM.Sdk.Enum;
using UM.Sdk.Model.Identity.Account.Request;
using UM.Sdk.Model.Identity.Account.Response;

namespace UM.ServiceProvider.Service.Identity
{
    public interface IAccountService
    {
        Task<RequestResult> RegisterAsync(RegisterRequest request,
            CancellationToken cancellationToken = default);

        Task<LoginResponse> LoginAsync(LoginRequest request,
            CancellationToken cancellationToken = default);
    }
}
