using System;
using System.Collections.Generic;
using System.Text;

namespace RequestProcessor.Core.Models
{
    public enum JobStatus
    {
        NOT_STARTED,
        STARTED,
        FINISHED_SUCCESS,
        FINISHED_FAILED,
        FINISHED_TIMEOUT
    } 
}
