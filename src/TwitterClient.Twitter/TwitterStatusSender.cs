using System;
using System.Collections.Generic;
using System.Linq;
using TweetSharp;
using TwitterClient.Interfaces;
using TwitterClient.Twitter.Properties;

namespace TwitterClient.Twitter
{
    public class TwitterStatusSender : IStatusSender
    {
        private TwitterService m_service;

        public TwitterStatusSender()
        {
            m_service = new TwitterService(Settings.Default.ConsumerKey, Settings.Default.ConsumerSecret);
            m_service.AuthenticateWith(Settings.Default.AuthenticationToken, Settings.Default.AuthenticationTokenSecret);
        }

        #region Implementation of IStatusSender

        public void SendStatusSync(string status)
        {
            m_service.SendTweet(new SendTweetOptions {Status = status});

            CheckForErrorAndThrowIfNecessary();
        }

        private void CheckForErrorAndThrowIfNecessary()
        {
            if (m_service.Response.Error != null)
                throw new Exception(m_service.Response.Error.Message);
        }

        public void SendStatusAsync(string status, Action<bool> done)
        {
            m_service.SendTweet(
                new SendTweetOptions {Status = status}, 
                (st, resp) => done(resp.Error == null));
        }

        public IEnumerable<ITimeLineEntry> GetTimeline(int nOfLastEntries)
        {
            var twitterTimeline = m_service.ListTweetsOnHomeTimeline(
                new ListTweetsOnHomeTimelineOptions {Count = nOfLastEntries});

            CheckForErrorAndThrowIfNecessary();

            return twitterTimeline.Select(t => new TwitterTimelineEntry(t.Text, t.CreatedDate)).ToList();
        }

        #endregion
    }
}