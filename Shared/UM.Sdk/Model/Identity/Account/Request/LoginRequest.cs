namespace UM.Sdk.Model.Identity.Account.Request
{
    /// <summary>
    /// Acceptable model for POST: api/identity/account/Login
    /// </summary>
    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
