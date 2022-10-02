namespace UM.DataAccess.Entity.Log
{
    /// <summary>
    /// Entity class represent all activity on the UM application
    /// </summary>
    public class UrlResolver
    {
        /// <summary>
        /// Represent the Primary key
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Represent the Request Host name
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// Represent the Request Path
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Represent the Response status code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Represent the Request Body Content
        /// </summary>
        public string BodyContent { get; set; } = string.Empty;

        /// <summary>
        /// Represent the Request User-Agent from header
        /// </summary>
        public string UserAgent { get; set; } = string.Empty;

        /// <summary>
        /// Represent the Request HTTP Verb
        /// </summary>
        public string Verb { get; set; } = string.Empty;

        /// <summary>
        /// Represent the content type
        /// </summary>
        public string ContentType { get; set; } = string.Empty;

        /// <summary>
        /// Represent the client IP Address
        /// </summary>
        public string Ip { get; set; } = string.Empty;

        /// <summary>
        /// Represnet the incoming request time
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Represent the Executing Time from starting process
        /// until providing the response content
        /// </summary>
        public double ExecuteTime { get; set; }
    }
}
