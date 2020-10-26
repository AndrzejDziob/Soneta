using System;
using System.Globalization;

namespace GitDataExchange
{
    public class DateTimeParser
    {
        /// <summary>
        /// Funkcja parsuje domyślny format daty w git na DateTime
        /// Format: Mon Jul 3 17:18:43 2006 +0200
        /// </summary>
        /// <param name="logText"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(string logText)
        {
            DateTime output = DateTime.MinValue;
            string[] logLineArray = logText.Split('\n');
            string dateString = logLineArray[2].Trim();
            dateString = dateString.Remove(0, 8);

            DateTimeOffset dateTimeOffset;
            if (DateTimeOffset.TryParseExact(dateString,
                                            "ddd MMM d HH:mm:ss yyyy K",
                                             CultureInfo.InvariantCulture,
                                             DateTimeStyles.None,
                                             out dateTimeOffset))
            {
                output = dateTimeOffset.DateTime;
            }

            return output;
        }
    }
}
