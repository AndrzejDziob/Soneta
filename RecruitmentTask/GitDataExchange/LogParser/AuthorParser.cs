using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitDataExchange
{
    public class AuthorParser
    {
        public static string GetAuthor(string logText)
        {
            string[] logLineArray = logText.Split('\n');
            string authorLine = logLineArray[1];
            int blankIndex = authorLine.IndexOf(' ');
            string author = authorLine.Substring(blankIndex + 1);
            return author;
        }
    }
}
