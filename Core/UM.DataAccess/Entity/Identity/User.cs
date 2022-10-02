using UM.DataAccess.Enum;

namespace UM.DataAccess.Entity.Identity
{
    /// <summary>
    /// Represent a domain entity for User 
    /// </summary>
    public class User
    {
        /// <summary>
        /// Rerpesent Primary Key for User Table
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Rerpesnet the username of user
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Rerpesnet the Email address of user
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Rerpesnet the First name of user
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Represent the Family name of the user
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Represent the old of this User
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Represent the Identification number for this user
        /// </summary>
        public string NationalCode { get; set; } = string.Empty;

        /// <summary>
        /// Represent the Gender of this user
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Represent the Current status of this user
        /// </summary>
        public UserStatus? Status { get; set; }

        /// <summary>
        /// Represent the user Password Hash
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Represent the user Password Salt
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Represent the user Password Salt
        /// </summary>
        public UserRole? Role { get; set; }

        /// <summary>
        /// Represent list of related addresses for this user
        /// </summary>
        public List<Address>? Addresses { get; set; } 

        /// <summary>
        /// Represent list of related phones for this user
        /// </summary>
        public List<Phone>? Phones { get; set; } 
    }
}
