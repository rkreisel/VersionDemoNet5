namespace VersionAPI.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Error Payload
    /// </summary>
    public class ErrorPayload
    {
        /// <summary>
        /// Gets or sets hTTP Status Code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets stack Trace
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// Gets or sets additional Data
        /// </summary>
#pragma warning disable CA2227 // Collection properties should be read only
        public Dictionary<string, string> Data { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
    }
}
