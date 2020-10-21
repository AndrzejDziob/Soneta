using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitDataExchange
{
    public class CommitLog
    {
        public CommitLog(string logText)
        {
            _logText = logText;
        }

        private readonly string _logText;

        public string Author
        {
            get
            {
                return AuthorParser.GetAuthor(_logText);
            }
        }

        public string CommitMsg
        {
            get
            {
                return CommitMsgParser.GetCommitMsg(_logText);
            }
        }

    }
}
