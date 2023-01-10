using Microsoft.Maui.Graphics;
using System.Drawing;
using System.Globalization;
using Color = Microsoft.Maui.Graphics.Color;

namespace Homiev2.Mobile.Converters
{
    public class ShowFutureChoresConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;
            if (dateTime.Date <= DateTime.Now.Date)
            {
                return true;
            }


            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
