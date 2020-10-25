using System;
using System.Globalization;

namespace GitDataExchange
{
    public class DateTimeParser
    {
        public static DateTime GetDateTime(string logText)
        {
            DateTime output = DateTime.MinValue;
            string[] logLineArray = logText.Split('\n');
            string dateString = logLineArray[2].Trim();
            dateString = dateString.Remove(0, 8);

            //string s = "Mon Jul 3 17:18:43 2006 +0200";
            DateTimeOffset dto;
            if (DateTimeOffset.TryParseExact(dateString,
                                            "ddd MMM d HH:mm:ss yyyy K",
                                             CultureInfo.InvariantCulture,
                                             DateTimeStyles.None,
                                             out dto))
            {
                output = dto.DateTime;
            }

            return output;
        }
    }
}
