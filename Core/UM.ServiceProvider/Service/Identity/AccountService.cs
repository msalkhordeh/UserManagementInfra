using UM.DataAccess.Entity.Identity;
using UM.DataAccess.Repository.Identity;
using UM.Sdk.Enum;
using UM.Sdk.Model;
using UM.Sdk.Model.Identity.Account.Request;
using UM.Sdk.Model.Identity.Account.Response;
using UM.ServiceProvider.InternalService.Authentication;
using UM.Utility;

namespace UM.ServiceProvider.Service.Identity
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtAuthenticationService _jwtAuthentication;

        public AccountService(IUserRepository userRepository, IUnitOfWork unitOfWork, IJwtAuthenticationService jwtAuthentication)
        {
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _jwtAuthentication = jwtAuthentication ??
                throw new ArgumentNullException(nameof(jwtAuthentication));
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request,
            CancellationToken cancellationToken = default)
        {
            LoginResponse result;
            var user = await _userRepository.GetByUsernameAndPassword(
                request.Username, request.Password, cancellationToken);
            if (user == null)
            {
                result = BaseResponseCollection
                    .GetBaseResponse<LoginResponse>(
                    RequestResult.InvalidCredential);
            }
            else
            {

                result = new LoginResponse
                {
                    Token = _jwtAuthentication.CreateTokenAuthentication(user)
                };
            }
            return result;
        }

        public async Task<RequestResult> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var result = RequestResult.SuccessfullCompleted;
            if (string.IsNullOrEmpty(request.Username)
                || string.IsNullOrEmpty(request.Password))
            {
                result = RequestResult.EmptyRequiredDataEntry;
            }
            else
            {
                var userExist = await _userRepository
                    .GetByUsername(request.Username);
                if (userExist != null)
                {
                    result = RequestResult.UsernameExist;
                }
                else
                {
                    var salt = Cryptography.GenerateSecret();
                    var user = new User
                    {
                        Username = request.Username,
                        PasswordSalt = salt,
                        PasswordHash = request.Password.CreateHash(salt)
                    };
                    await _userRepository.Add(user, cancellationToken);
                    await _unitOfWork.CompleteAsync(cancellationToken);
                }
            }
            return result;
        }
    }
}
