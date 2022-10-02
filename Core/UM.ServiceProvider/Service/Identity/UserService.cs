using AutoMapper;
using UM.DataAccess.Entity.Identity;
using UM.DataAccess.Repository.Identity;
using UM.Sdk.Enum;
using UM.Sdk.Model;
using UM.Sdk.Model.Identity.User.Request;
using UM.Sdk.Model.Identity.User.Response;

namespace UM.ServiceProvider.Service.Identity
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddAsync(
            AddRequest request,
            CancellationToken cancellationToken = default)
        {
            var user = _mapper.Map<User>(request);
            user = await _userRepository.Add(user, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return user.Id;
        }

        public async Task<UpdateResponse> UpdateAsync(UpdateRequest request,
            CancellationToken cancellationToken = default)
        {
            UpdateResponse response;
            var user = await _userRepository.GetById(request.UserId,
                cancellationToken);
            if (user == null)
            {
                response = BaseResponseCollection.GetBaseResponse<UpdateResponse>(
                    RequestResult.InvalidUserId);
            }
            else
            {
                user = _mapper.Map(request, user);
                user = _userRepository.Update(user);
                response = new UpdateResponse
                {
                    UserId = user.Id
                };
            }
            await _unitOfWork.CompleteAsync(cancellationToken);

            return response;
        }

        public async Task<RequestResult> DeleteAsync(int userId,
            CancellationToken cancellationToken = default)
        {
            RequestResult result;
            var user = await _userRepository.GetById(userId, cancellationToken);
            if (user == null)
            {
                result = RequestResult.InvalidUserId;
            }
            else
            {
                _userRepository.Delete(user);
                result = RequestResult.SuccessfullCompleted;
            }

            await _unitOfWork.CompleteAsync(cancellationToken);
            return result;
        }

        public async Task<GetAllResponse> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var users = await _userRepository.GetAll(cancellationToken);
            return new GetAllResponse
            {
                Users = _mapper.Map<IEnumerable<Sdk.Model.Identity.User.User>>(
                    users)
            };
        }
    }
}
