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

        private string RunCommand(string args)
        {
            _gitProcess.StartInfo.Arguments = args;
            _gitProcess.Start();
            string output = _gitProcess.StandardOutput.ReadToEnd().Trim();
            _gitProcess.WaitForExit();
            return output;
        }


        public IEnumerable<string> Log
        {
            get
            {
                int skip = 0;
                while (true)
                {
                    string entry = RunCommand(String.Format("log --skip={0} -n1", skip++));
                    if (String.IsNullOrWhiteSpace(entry))
                    {
                        yield break;
                    }

                    yield return entry;
                }
            }
        }

    }
}
