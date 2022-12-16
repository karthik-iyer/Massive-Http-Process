using System;
using System.Collections.Generic;
using System.Text;

namespace RequestProcessor.Core.Models
{
    /// <summary>
    /// Response model for checking job status by Job Id
    /// </summary>
    public class HttpResponseModel
    {
        /// <summary>
        /// Requesting JobId for the Job Status
        /// </summary>
        public string JobId { get; set; }

        /// <summary>
        /// Job Status for the corresponding JobId
        /// </summary>
        public string JobStatus { get; set; }

        /// <summary>
        /// HttpResponse String
        /// </summary>
        public string HttpResponseString { get; set; }

        /// <summary>
        /// Http Response code for the job
        /// </summary>
        public string HttpResponseCode { get; set; }
    }
}
