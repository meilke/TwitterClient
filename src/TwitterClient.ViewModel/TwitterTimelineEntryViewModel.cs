using System;
using System.Windows;

namespace TwitterClient.ViewModel
{
    public class TwitterTimelineEntryViewModel : DependencyObject
    {
        public string Status
        {
            get { return (string)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(string), typeof(TwitterTimelineEntryViewModel), new UIPropertyMetadata(string.Empty));

        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register("Date", typeof(DateTime), typeof(TwitterTimelineEntryViewModel));
    }
}