using GitDataExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryInfoAddon
{
    public class GitDataProviderBuilder
    {
        public static GitDataProvider GetGitDataProvider(string repositoryPath, string gitPath = null)
        {
            var gitProcess = new GitProcess(repositoryPath);
            var gitDataProvider = new GitDataProvider(gitProcess);
            return gitDataProvider;
        }
    }
}
