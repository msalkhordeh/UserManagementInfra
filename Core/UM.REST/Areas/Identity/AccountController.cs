using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UM.Sdk.Enum;
using UM.Sdk.Model;
using UM.Sdk.Model.Identity.Account.Request;
using UM.Sdk.Model.Identity.Account.Response;
using UM.ServiceProvider.Service.Identity;

namespace UM.REST.Areas.Identity
{
    [ApiController]
    [Route("api/identity/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService
                ?? throw new ArgumentNullException(nameof(accountService));
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BaseResponse>> RegisterAsync
            ([FromBody] RegisterRequest request,
            CancellationToken cancellationToken = default)
        {
            ActionResult<BaseResponse> response;
            try
            {
                RequestResult result = await _accountService
                    .RegisterAsync(request, cancellationToken);
                response = result == RequestResult.SuccessfullCompleted
                    ? Ok(new BaseResponse())
                    : BadRequest(BaseResponseCollection.GetBaseResponse(result));
            }
            catch
            {
                response =StatusCode(400, BaseResponseCollection.GetBaseResponse(
                    RequestResult.UnhandledExceptionError));
            }
            return response;
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginResponse>> LoginAsync
            ([FromBody] LoginRequest request,
            CancellationToken cancellationToken = default)
        {
            ActionResult<LoginResponse> response;
            try
            {
                var result = await _accountService
                    .LoginAsync(request, cancellationToken);
                response = result.ResultCode == RequestResult.SuccessfullCompleted
                    ? Ok(result)
                    : Unauthorized(BaseResponseCollection
                    .GetBaseResponse(result.ResultCode));
            }
            catch
            {
                response = StatusCode(400, BaseResponseCollection.GetBaseResponse(
                 RequestResult.UnhandledExceptionError));
            }
            return response;
        }
    }
}
