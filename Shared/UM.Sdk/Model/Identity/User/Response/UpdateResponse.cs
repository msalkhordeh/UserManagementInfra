namespace UM.Sdk.Model.Identity.User.Response
{
    /// <summary>
    /// Represent the Acceptable response model for:
    /// PUT api/identity/user/Update
    /// </summary>
    public class UpdateResponse : BaseResponse
    {
        public int UserId { get; set; }
    }
}
