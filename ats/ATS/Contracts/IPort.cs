using System;
using System.ComponentModel;

namespace ats.ats.Contracts
{
    public interface IPort
    {
        PortState PortState { get; set; }

        event EventHandler StateChanged;
        void RegisterEventHandlersForPphone(IPhone phone);
    }
}
