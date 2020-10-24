using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitDataExchange
{
    public class GitDataProvider
    {
        public GitDataProvider(GitProcess gitProcess)
        {
            _gitProcess = gitProcess.Process;
        }

        private readonly Process _gitProcess;

        public bool HasProperAddress
        {
            get
            {
                return !String.IsNullOrWhiteSpace(RunCommand("log -1"));
            }
        }

        public IEnumerable<CommitLog> Log
        {
            get
            {
                int skip = 0;
                while (true)
                {
                    string entry = RunCommand(String.Format("log --all --skip={0} -n1", skip++));
                    //string entry = RunCommand(String.Format("log --skip={0} -n1", skip++));
                    if (String.IsNullOrWhiteSpace(entry))
                    {
                        yield break;
                    }

                    yield return new CommitLog(entry);
                }
            }
        }

        private string RunCommand(string args)
        {
            _gitProcess.StartInfo.Arguments = args;
            _gitProcess.Start();
            string output = _gitProcess.StandardOutput.ReadToEnd().Trim();
            _gitProcess.WaitForExit();
            return output;
        }

    }
}
