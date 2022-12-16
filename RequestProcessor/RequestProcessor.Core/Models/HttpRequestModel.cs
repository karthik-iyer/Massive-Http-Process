using System;

namespace RequestProcessor.Core.Models
{
    public class HttpRequestModel
    {
        public string JobName { get; set; }

        public Guid ClientId { get; set; }

        public string Url { get; set; }

        public string HttpMethod { get; set; }

        public string ContentType { get; set; }

        public string RequestBody { get; set; }

        public string WebHookUrl { get; set; }
    }
}
