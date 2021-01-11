using ats.ATS.States;
namespace ats.ATS
{
    public class CallEventArg
    {
        public string SourcePhoneNumber { get; set; }
        public string TargetPhoneNumber { get; set; }
        public CallState State { get; set; }
    }
}
