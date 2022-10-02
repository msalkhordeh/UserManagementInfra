using UM.Sdk.Enum;

namespace UM.Sdk.Model
{
    /// <summary>
    /// Represent the Base Response model for All Response 
    /// object exist in the UM(should Inherit)
    /// </summary>
    public class BaseResponse
    {
        public RequestResult ResultCode { get; set; } 
            = RequestResult.SuccessfullCompleted;

        public string Message { get; set; }
            = "Successfully Executed.";
    }
}
