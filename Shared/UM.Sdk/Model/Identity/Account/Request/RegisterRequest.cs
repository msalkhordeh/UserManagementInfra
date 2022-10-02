namespace UM.Sdk.Model.Identity.Account.Response
{
    /// <summary>
    /// Acceptable model for POST: api/identity/account/Register
    /// </summary>
    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
