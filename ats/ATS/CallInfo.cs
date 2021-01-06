using ats.ATS.States;
using System;

namespace ats.ATS
{
    public class CallInfo
    {
        public string To { get; set; }

        public string From { get; set; }

        public DateTime CallDate { get; set; }

        public TimeSpan Duration { get; set; }

        public CallState CallState { get; set; }
    }
}
