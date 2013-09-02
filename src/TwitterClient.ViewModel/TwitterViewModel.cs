using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using TwitterClient.Model;

namespace TwitterClient.ViewModel
{
    public class TwitterViewModel : DependencyObject
    {
        private TwitterModel m_model;
        private Timer m_timer;

        public TwitterViewModel()
        {
            m_model = new TwitterModel();

            StatusText = "What's going on?";
            GoCommand = new RelayCommand(OnGoClicked);
            TimelineEntries = new ObservableCollection<TwitterTimelineEntryViewModel>();

            m_timer = new Timer(OnTime, null, 0, 300000);
        }

        private void OnTime(object state)
        {
            UpdateTimeline();
        }

        private void UpdateTimeline()
        {
            Action action = () =>
                                {
                                    try
                                    {
                                        TimelineEntries.Clear();

                                        foreach (var model in m_model.GetTimeline(5))
                                            TimelineEntries.Add(new TwitterTimelineEntryViewModel
                                                                    {Date = model.Date, Status = model.Status});
                                    }
                                    catch (Exception e)
                                    {
                                        MessageBox.Show(string.Format("WTF?{0}{1}", Environment.NewLine, e.Message));
                                    }
                                };

            Dispatcher.Invoke(action);
        }

        public ObservableCollection<TwitterTimelineEntryViewModel> TimelineEntries
        {
            get { return (ObservableCollection<TwitterTimelineEntryViewModel>)GetValue(TimelineEntriesProperty); }
            set { SetValue(TimelineEntriesProperty, value); }
        }

        public static readonly DependencyProperty TimelineEntriesProperty =
            DependencyProperty.Register("TimelineEntries", typeof(ObservableCollection<TwitterTimelineEntryViewModel>), typeof(TwitterViewModel));

        public bool IsNotBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsNotBusy", typeof(bool), typeof(TwitterViewModel), new UIPropertyMetadata(true));

        public string StatusText
        {
            get { return (string)GetValue(StatusTextProperty); }
            set { SetValue(StatusTextProperty, value); }
        }

        public static readonly DependencyProperty StatusTextProperty =
            DependencyProperty.Register("StatusText", typeof(string), typeof(TwitterViewModel), new UIPropertyMetadata(string.Empty));

        public ICommand GoCommand
        {
            get { return (ICommand)GetValue(GoCommandProperty); }
            set { SetValue(GoCommandProperty, value); }
        }

        public static readonly DependencyProperty GoCommandProperty =
            DependencyProperty.Register("GoCommand", typeof(ICommand), typeof(TwitterViewModel));

        private void OnGoClicked()
        {
            IsNotBusy = false;

            try
            {
                m_model.SendStatusAsync(StatusText, OnDone);
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("WTF?{0}{1}", Environment.NewLine, e.Message));
                IsNotBusy = true;
            }
        }

        private void OnDone(bool success)
        {
            if (!success)
                MessageBox.Show(string.Format("WTF?"));

            Action action = () =>
                                {
                                    UpdateTimeline();
                                    IsNotBusy = true;
                                };

            Dispatcher.Invoke(action);
        }
    }
}