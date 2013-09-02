using System;
using System.Collections.Generic;

namespace TwitterClient.Interfaces
{
    public interface IStatusSender
    {
        void SendStatusSync(string status);

        void SendStatusAsync(string status, Action<bool> done);

        IEnumerable<ITimeLineEntry> GetTimeline(int nOfLastEntries);
    }
}