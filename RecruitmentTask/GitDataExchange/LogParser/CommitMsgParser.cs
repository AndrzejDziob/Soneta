using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitDataExchange
{
    public class CommitMsgParser
    {
        public static string GetCommitMsg(string logText)
        {
            string[] logLineArray = logText.Split('\n');
            string msgLine = logLineArray[4].Trim();
            return msgLine;
        }
    }
}
