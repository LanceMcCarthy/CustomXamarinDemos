using System;
using System.Globalization;
using Xamarin.Forms;

namespace CallDetector.Portable.Converters
{
    internal class LineTypeDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "mobile":
                    return "Mobile Phone";
                case "landline":
                    return "Landline";
                case "special_services":
                    return "Special Services (e.g. Police)";
                case "toll_free":
                    return "Toll-Free Numbers (e.g. hotels)";
                case "premium_rate":
                    return "Premium Rate Numbers (e.g. paid hotlines)";
                case "satellite":
                    return "Satellite";
                case "paging":
                    return "Paging";
                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "Mobile Phone":
                    return "mobile";
                case "Landline":
                    return "landline";
                case "Special Services (e.g. Police)":
                    return "special_services";
                case "Toll-Free Numbers (e.g. hotels)":
                    return "toll_free";
                case "Premium Rate Numbers (e.g. paid hotlines)":
                    return "premium_rate";
                case "Satellite":
                    return "satellite";
                case "Paging":
                    return "paging";
                default:
                    return "";
            }
        }
    }
}