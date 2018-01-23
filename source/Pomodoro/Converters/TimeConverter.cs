using System;
using System.Globalization;
using System.Windows.Data;

namespace Pomodoro.Converters
{
   [ValueConversion(typeof(TimeSpan), typeof(string))]
   internal class TimeConverter : IValueConverter
   {
      object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         TimeSpan span = TimeSpan.Parse(value.ToString());

         //if (span.Seconds < 10) return span.Minutes + " : 0" + span.Seconds;
         //else return span.Minutes + " : " + span.Seconds;
         if (span.Hours > 0) return span.ToString(@"hh\:mm\:ss");
         else return span.ToString(@"mm\:ss");
      }

      object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}