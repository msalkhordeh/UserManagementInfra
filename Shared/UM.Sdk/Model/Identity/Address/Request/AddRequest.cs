using UM.Sdk.Enum;

namespace UM.Sdk.Model.Identity.Address.Request
{
    /// <summary>
    /// Represent the Acceptable request model for:
    /// POST api/identity/address/Add
    /// </summary>
    public class AddRequest
    {
        /// <summary>
        /// Represent The Owner User for this address
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Represent the Hole Address
        /// </summary>
        public string FullAddress { get; set; } = string.Empty;

        /// <summary>
        /// Represent City Address Not Contain in the <see cref="FullAddress"/>
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// Represent Country Address Not Contain in the <see cref="FullAddress"/>
        /// </summary>
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// Represent the Type of address
        /// </summary>
        public AddressType Type { get; set; }

        /// <summary>
        /// Represent the Postal Code as ISO
        /// </summary>
        public string PostalCode { get; set; } = string.Empty;
    }
}
