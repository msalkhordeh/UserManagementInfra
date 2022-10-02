using UM.Sdk.Enum;

namespace UM.Sdk.Model.Identity.User.Request
{
    /// <summary>
    /// Represent the Acceptable request model for:
    /// PUT api/identity/user/Update
    /// </summary>
    public class UpdateRequest
    {
        /// <summary>
        /// Represent the Primary Key of User
        /// </summary>
        public int UserId { get; set; }

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
        public Gender Gender { get; set; }
    }
}
