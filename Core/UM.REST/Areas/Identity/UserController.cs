using Microsoft.AspNetCore.Mvc;
using UM.Sdk.Enum;
using UM.Sdk.Model;
using UM.Sdk.Model.Identity.User.Request;
using UM.Sdk.Model.Identity.User.Response;
using UM.ServiceProvider.InternalService.Authentication;
using UM.ServiceProvider.Service.Identity;

namespace UM.REST.Areas.Identity
{
    [ApiController]
    [Route("api/identity/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthUser _authUser;

        public UserController(IUserService userService, IAuthUser authUser)
        {
            _userService = userService
                ?? throw new ArgumentNullException(nameof(userService));
            _authUser = authUser
                ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddResponse>> AddAsync(
            [FromBody] AddRequest request,
            CancellationToken cancellationToken = default)
        {
            ActionResult<AddResponse> response;
            try
            {
                var userId = await _userService.AddAsync(
                    request, cancellationToken);
                response = Ok(new AddResponse
                {
                    UserId = userId
                });
            }
            catch
            {
                response = StatusCode(500,
                    BaseResponseCollection.
                    GetBaseResponse(RequestResult.UnhandledExceptionError));
            }
            return response;
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UpdateResponse>> UpdateAsync(
            [FromBody] UpdateRequest request,
            CancellationToken cancellationToken = default)
        {
            ActionResult<UpdateResponse> response;
            try
            {
                var result = await _userService
                    .UpdateAsync(request, cancellationToken);
                response = result.ResultCode == 0 ?
                    Ok(result) :
                    NotFound(BaseResponseCollection
                    .GetBaseResponse(result.ResultCode));
            }
            catch
            {
                response = StatusCode(500,
                    BaseResponseCollection.
                    GetBaseResponse(RequestResult.UnhandledExceptionError));
            }
            return response;
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseResponse>> DeleteAsync(
            int userId, CancellationToken cancellationToken = default)
        {
            ActionResult<BaseResponse> response;
            try
            {
                var result = await _userService
                    .DeleteAsync(userId, cancellationToken);
                response = result == RequestResult.SuccessfullCompleted
                    ? Ok(new BaseResponse())
                    : NotFound(result);
            }
            catch
            {
                response = StatusCode(500,
                    BaseResponseCollection.
                    GetBaseResponse(RequestResult.UnhandledExceptionError));
            }
            return response;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<GetAllResponse>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            ActionResult<GetAllResponse> response;
            try
            {
                if (_authUser.User?.Username.ToLower() == "milad")
                {
                    response = StatusCode(403, "Milad Is Forbidden!!!");
                }
                else
                {
                    var result = await _userService
                    .GetAllAsync(cancellationToken);
                    response = Ok(result);
                }
            }
            catch
            {
                response = StatusCode(500,
                    BaseResponseCollection
                    .GetBaseResponse(RequestResult.UnhandledExceptionError));
            }
            return response;
        }
    }
}
