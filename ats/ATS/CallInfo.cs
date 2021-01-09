using ats.ATS.States;
using System;
using System.Text;

namespace ats.ATS
{
    public class CallInfo
    {
        public string To { get; set; }
        public string From { get; set; }
        public DateTime CallDate { get; set; }
        public TimeSpan Duration { get; set; }
        public CallState CallState { get; set; }
        public override string ToString()
        {
            return "CallState: " + CallState + "\tCall date: " + CallDate.TimeOfDay + "\tDuration: " + Duration;
        }
    }
}
