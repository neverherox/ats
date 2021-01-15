using ats.ATS.States;
using System;

namespace ats.ATS
{
    public class CallEventArgs : EventArgs
    {
        public string SourcePhoneNumber { get; set; }
        public string TargetPhoneNumber { get; set; }
        public CallState State { get; set; }
    }
}
