using System;
using System.Collections.Generic;
using System.Text;

namespace RequestProcessor.Core.Models
{
    public class JobConfiguration
    {
        public int NumberOfConcurrentJobs { get; set; } = 8;
    }
}
