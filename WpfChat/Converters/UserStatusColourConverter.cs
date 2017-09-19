using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using WpfChat.Stores;

namespace WpfChat.Converters
{
    [ValueConversion(typeof(ChatUserStatus), typeof(string))]
    class UserStatusColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (ChatUserStatus)value;
            switch (status)
            {
                case ChatUserStatus.Offline:
                    return "Grey";
                case ChatUserStatus.Away:
                    return "Khaki";
                case ChatUserStatus.Online:
                    return "Green";
                default:
                    throw new Exception("Unknown Status");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
