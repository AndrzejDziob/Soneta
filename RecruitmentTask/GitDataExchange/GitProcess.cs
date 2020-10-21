using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitDataExchange
{
    public class GitProcess
    {
        public GitProcess(string repositoryPath, string gitPath = null)
        {
            var processInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = Directory.Exists(gitPath) ? gitPath : "git.exe",
                CreateNoWindow = true,
                WorkingDirectory = (repositoryPath != null && Directory.Exists(repositoryPath)) ? repositoryPath : Environment.CurrentDirectory
            };

            Process = new Process
            {
                StartInfo = processInfo
            };
        }

        public Process Process { get; }
    }
}
