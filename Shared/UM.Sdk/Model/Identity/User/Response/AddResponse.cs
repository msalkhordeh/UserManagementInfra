namespace UM.Sdk.Model.Identity.User.Response
{
    /// <summary>
    /// Represent the Acceptable response model for:
    /// POST api/identity/user/Add
    /// </summary>
    public class AddResponse : BaseResponse
    {
        public int UserId { get; set; }
    }
}
