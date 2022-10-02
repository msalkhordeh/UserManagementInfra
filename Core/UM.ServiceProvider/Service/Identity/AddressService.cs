using AutoMapper;
using UM.DataAccess.Entity.Identity;
using UM.DataAccess.Repository.Identity;
using UM.Sdk.Enum;
using UM.Sdk.Model;
using UM.Sdk.Model.Identity.Address.Request;
using UM.Sdk.Model.Identity.Address.Response;

namespace UM.ServiceProvider.Service.Identity
{
    public class AddressService : IAddressService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;

        public AddressService(IMapper mapper, 
            IUserRepository userRepository,
            IAddressRepository addressRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AddResponse> AddAsync(AddRequest request,
            CancellationToken cancellationToken = default)
        {
            AddResponse addResponse;
            var user = await _userRepository.GetById(request.UserId, cancellationToken);
            if (user != null)
            {
                var address = _mapper.Map<Address>(request);
                await _addressRepository.Add(address, cancellationToken);
                await _unitOfWork.CompleteAsync(cancellationToken);
                return new AddResponse
                {
                    AddressId = address.Id
                };
            }
            else
            {
                addResponse = BaseResponseCollection
                    .GetBaseResponse<AddResponse>(RequestResult.InvalidUserId);
            }
            return addResponse;
        }
    }
}
