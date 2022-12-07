using System.Globalization;

namespace Domain.Parsers
{
    public static class DateTimeParser
    {
        public const string DefaultDateTimePattern = "dd/MM/yyyy HH:mm:ss";
        public const string DefaultIntDateTime = "dd/MM/yyyy";


        public static DateTime Parse(string date) 
        {
            if (DateTime.TryParseExact(date, DefaultDateTimePattern, 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var formatDate))
            {
                return formatDate;
            }
            else if (DateTime.TryParseExact(date, DefaultIntDateTime, 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var format))
            {
                return format;
            }

            throw new ArgumentException("A Data é inválida");
        }
    }
}
