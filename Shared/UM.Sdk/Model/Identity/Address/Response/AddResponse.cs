namespace UM.Sdk.Model.Identity.Address.Response
{
    /// <summary>
    /// Represent the Acceptable response model for:
    /// POST api/identity/Address/Add
    /// </summary>
    public class AddResponse : BaseResponse
    {
        public int AddressId { get; set; }
    }
}
