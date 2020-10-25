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
            string commitMsg = string.Empty;
            short msgLineIndex = 4;

            for (short i = msgLineIndex; i<logLineArray.Length; i++)
            {
                string line = logLineArray[i].Trim();
                if (!string.IsNullOrWhiteSpace(line))
                    commitMsg += $"{line}\n";
            }

            return commitMsg;
        }
    }
}
