using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Mobile.Converters
{
    public class DayofWeekToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                int dayOfWeek = (int)value - 1;
                return dayOfWeek;
            }
            return -1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                DayOfWeek dayOfWeek = (DayOfWeek)value + 1;
                return dayOfWeek;
            }
            return -1;
        }
    }
}
