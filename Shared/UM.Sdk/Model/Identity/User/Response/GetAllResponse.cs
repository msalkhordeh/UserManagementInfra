namespace UM.Sdk.Model.Identity.User.Response
{
    /// <summary>
    /// Acceptable model for GET: api/identity/user/GetAll
    /// </summary>
    public class GetAllResponse : BaseResponse
    {
        public IEnumerable<User> Users { get; set; } = new HashSet<User>();
    }
}
