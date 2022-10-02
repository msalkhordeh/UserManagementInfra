namespace UM.Sdk.Model.Identity.Account.Response
{
    /// <summary>
    /// Acceptable model for Response POST: api/identity/account/Login
    /// </summary>
    public class LoginResponse : BaseResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}
