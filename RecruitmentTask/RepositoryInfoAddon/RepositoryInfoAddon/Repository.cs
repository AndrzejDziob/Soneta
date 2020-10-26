using System;
using System.Collections.Generic;
using System.Linq;
using GitDataExchange;

namespace RepositoryInfoAddon
{
    public class Repository
    {
        public Repository(string repositoryPath, string gitPath = null)
        {
            _gitDataProvider = GitDataProviderBuilder.GetGitDataProvider(repositoryPath, gitPath);
            Commits = new List<Commit>();
        }

        private GitDataProvider _gitDataProvider;

        public bool HasProperAddress => _gitDataProvider.HasProperAddress;

        public void FetchCommitData()
        {
            Commits.Clear();

            var logs = _gitDataProvider.Log;

            foreach (var log in logs)
            {
                var commit = new Commit();
                var author = new Author
                {
                    Name = Author.ExtractName(log.Author),
                    Email = Author.ExtractEmail(log.Author)
                };
                commit.Author = author;
                commit.Message = log.CommitMsg;
                commit.DateTime = log.DateTime;

                Commits.Add(commit);
            }
        }

        public List<Commit> Commits { get; }

        public List<Author> Authors
        {
            get
            {
                var authors = Commits.Select(x => x.Author);
                return authors.GroupBy(x => x.Email).Select(y => y.First()).ToList();
            }
        }


    }
}
