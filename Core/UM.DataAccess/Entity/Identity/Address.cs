using UM.DataAccess.Enum;

namespace UM.DataAccess.Entity.Identity
{
    /// <summary>
    /// Represent a domain entity for Address 
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Represent Primary Key Address
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Represent The Owner User for this address
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Related User Model
        /// </summary>
        public User User { get; set; } = new();

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
        public AddressType? Type { get; set; }

        /// <summary>
        /// Represent the Postal Code as ISO
        /// </summary>
        public string PostalCode { get; set; } = string.Empty;
    }
}
