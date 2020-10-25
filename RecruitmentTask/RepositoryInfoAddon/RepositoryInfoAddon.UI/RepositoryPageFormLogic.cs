using System;
using System.Collections.Generic;

namespace RepositoryInfoAddon.UI
{
    internal class RepositoryPageFormLogic
    {
        public RepositoryPageFormLogic(string repositoryPath)
        {
            _repository = new Repository(repositoryPath);
        }

        private Repository _repository;

        public bool HasProperAddress => _repository.HasProperAddress;

        public void InitRepositoryData()
        {
            _repository.FetchCommitData();
        }

        public List<AuthorRow> GetAuthors()
        {
            var output = new List<AuthorRow>();
            foreach (var author in _repository.Authors)
                output.Add(new AuthorRow { Name = author.Name, Email = author.Email });
            return output;
        }

        public List<CommitRow> GetCommits(string email)
        {
            var output = new List<CommitRow>();
            var filteredCommits = _repository.Commits.FindAll(x => x.Author.Email == email);
            foreach (var commit in filteredCommits)
                output.Add(new CommitRow { Message = commit.Message, DateTime = commit.DateTime.ToString() });
            return output;
        }

        public int GetCommitCount(string email, DateTime date)
        {
            var filteredCommits = _repository.Commits.FindAll(x => x.Author.Email == email && x.DateTime.Date == date);
            return filteredCommits.Count;
        }

        public double GetAverageCommitCount(string email, DateTime dateFrom, DateTime dateTo, out bool isValidRange)
        {
            double output = 0;
            var filteredCommits = _repository.Commits.FindAll(x => x.Author.Email == email &&
                                                                    dateFrom <= x.DateTime.Date && x.DateTime.Date <= dateTo);

            int rangeDiff = (dateTo - dateFrom).Days;
            isValidRange = rangeDiff >= 0;

            if (isValidRange)
            {
                int numberOfDaysInRage = rangeDiff + 1;
                output = filteredCommits.Count / (double)numberOfDaysInRage;
            }

            return output;
        }
    }
}
