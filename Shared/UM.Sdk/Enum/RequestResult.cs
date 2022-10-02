namespace UM.Sdk.Enum
{
    /// <summary>
    /// All Result code of the current context
    /// </summary>
    public enum RequestResult : int
    {
        /// <summary>
        /// Successfully Executed.
        /// </summary>
        SuccessfullCompleted,

        /// <summary>
        /// Something Unexpected occured in the server please contact support.
        /// </summary>
        UnhandledExceptionError,

        /// <summary>
        /// Wrong User Id.
        /// </summary>
        InvalidUserId,

        /// <summary>
        /// Please fill all required parameters in the request.
        /// </summary>
        EmptyRequiredDataEntry,

        /// <summary>
        /// Username already exist in the system.
        /// </summary>
        UsernameExist,

        /// <summary>
        /// Username or password Wrong.
        /// </summary>
        InvalidCredential,


        /// <summary>
        /// Invalid Credential.
        /// </summary>
        InvalidJwt,
    }
}
