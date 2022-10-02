using UM.DataAccess.Enum;

namespace UM.DataAccess.Entity.Identity
{
    /// <summary>
    /// Represent Domain for Phone Entity
    /// </summary>
    public class Phone
    {
        /// <summary>
        /// Represent The PK 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Represent the Phone type
        /// </summary>
        public PhoneType? Type { get; set; }

        /// <summary>
        /// Represent the Number of phone
        /// </summary>
        public string Number { get; set; }=string.Empty;

        /// <summary>
        /// Represent the Country Code ISO number
        /// </summary>
        public string CountryCode { get; set; } = string.Empty;

        /// <summary>
        /// Represent the FK of the User Entity
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Represent related User Entity
        /// </summary>
        public User User { get; set; } = new();
    }
}
