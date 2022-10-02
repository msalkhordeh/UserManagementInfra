namespace UM.Sdk.Enum
{
    /// <summary>
    /// Collection of all possible status 
    /// for any user account exist in UM system
    /// </summary>
    public enum UserStatus : byte
    {
        /// <summary>
        /// Represent an Inactive status that disallowed working with UM
        /// </summary>
        InActive = 0,

        /// <summary>
        /// Represent an active status that allow working with UM
        /// </summary>
        Active = 1,

        /// <summary>
        /// Represent a freezing status that block 
        /// User From Normal Activity in the system
        /// </summary>
        Freeze = 2,
    }
}
