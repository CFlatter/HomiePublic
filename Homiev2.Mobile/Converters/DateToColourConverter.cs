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
            if (dateTime < DateTime.Now)
            {
                var backgroundColour = Color.FromArgb("#ffc91512");
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
