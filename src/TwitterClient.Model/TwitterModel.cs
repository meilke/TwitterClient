using System;
using System.Collections.Generic;
using TwitterClient.Interfaces;
using TwitterClient.Twitter;

namespace TwitterClient.Model
{
    public class TwitterModel
    {
        private IStatusSender m_statusClient;

        public TwitterModel()
        {
            m_statusClient = new TwitterStatusSender();
        }

        public void SendStatusSync(string status)
        {
            m_statusClient.SendStatusSync(status);
        }

        public void SendStatusAsync(string status, Action<bool> done)
        {
            m_statusClient.SendStatusAsync(status, done);
        }

        public IEnumerable<ITimeLineEntry> GetTimeline(int nOfLastEntries)
        {
            return m_statusClient.GetTimeline(nOfLastEntries);
        }
    }
}