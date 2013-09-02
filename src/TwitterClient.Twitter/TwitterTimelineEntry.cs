using System;
using TwitterClient.Interfaces;

namespace TwitterClient.Twitter
{
    public class TwitterTimelineEntry : ITimeLineEntry
    {
        public TwitterTimelineEntry(string status, DateTime date)
        {
            Status = status;
            Date = date;
        }

        #region Implementation of ITimeLineEntry

        public string Status { get; private set; }
        public DateTime Date { get; private set; }

        #endregion
    }
}