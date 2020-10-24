using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitDataExchange;

namespace RepositoryInfoAddon
{
    public class Repository
    {
        public Repository(string repositoryPath, string gitPath = null)
        {
            _gitDataProvider = GitDataProviderBuilder.GetGitDataProvider(repositoryPath, gitPath);
            //_shouldRefreshData = true;
        }

        private GitDataProvider _gitDataProvider;
        private List<Commit> _commits;
        private List<Author> _authors;
        //private bool _shouldRefreshData;

        //public bool ShouldRefreshData
        //{
        //    set { _shouldRefreshData = value; }
        //}

        public bool HasProperAddress
        {
            get
            {
                return _gitDataProvider.HasProperAddress;
            }
        }

        public List<Commit> Commits
        {
            get
            {
                if (_commits == null)
                {
                    _commits = new List<Commit>();
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

                        _commits.Add(commit);
                    }
                }
                return _commits;
            }
        }

        public List<Author> Authors
        {
            get
            {
                if(_authors == null)
                {
                    _authors = new List<Author>();
                    var authors = Commits.Select(x => x.Author);
                    _authors = authors.GroupBy(x => x.Email).Select(y => y.First()).ToList();
                }
                return _authors;
            }
        }

    }
}
