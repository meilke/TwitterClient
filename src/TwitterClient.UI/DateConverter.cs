using System;
using System.Globalization;
using System.Windows.Data;

namespace TwitterClient.UI
{
    public class DateConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime) value;
            DateTime now = DateTime.Now;

            var sinceDate = now - date;

            return string.Format("{0:0.0}h", sinceDate.TotalHours);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}