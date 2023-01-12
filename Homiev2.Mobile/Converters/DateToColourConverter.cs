using Microsoft.Maui.Graphics;
using System.Drawing;
using System.Globalization;
using Color = Microsoft.Maui.Graphics.Color;

namespace Homiev2.Mobile.Converters
{
    public class DateToColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;
            if (dateTime.Date < DateTime.Now.Date)
            {
                var backgroundColour = Color.FromArgb("#9AFF1512");
                return backgroundColour;
            }


            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
