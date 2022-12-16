using System;
using System.Collections.Generic;
using System.Text;

namespace RequestProcessor.Core.Models
{
    public class JobModel
    {
        public string JobName { get; set; }

        public int ClientId { get; set; }

        public string Url { get; set; }

        public string HttpMethod { get; set; }

        public string ContentType { get; set; }

        public string RequestBody { get; set; }

        public string WebHookUrl { get; set; }

        public JobStatus CurrentJobStatus { get; set; }

        public string JobId { get; set; }
    }
}
