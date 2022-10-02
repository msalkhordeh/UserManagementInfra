using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UM.Sdk.Enum;
using UM.Sdk.Model;
using UM.Sdk.Model.Identity.Address.Request;
using UM.Sdk.Model.Identity.Address.Response;
using UM.ServiceProvider.Service.Identity;

namespace UM.REST.Areas.Identity
{
    [ApiController]
    [Route("api/identity/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddResponse>> AddAsync(
            [FromBody] AddRequest request, CancellationToken cancellationToken = default)
        {
            ActionResult<AddResponse> response;
            try
            {
                var result = await _addressService.AddAsync(request,
                    cancellationToken);
                response = result.ResultCode == RequestResult.SuccessfullCompleted
                    ? Ok(result)
                    : NotFound(BaseResponseCollection
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
    }
}
