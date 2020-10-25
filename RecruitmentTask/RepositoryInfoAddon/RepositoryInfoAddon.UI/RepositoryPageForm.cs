using Soneta.Business;
using Soneta.Business.App;

using Soneta.Business.UI;
using RepositoryInfoAddon.UI;
using System.Collections.Generic;
using System;
using Soneta.BI;

namespace RepositoryInfoAddon.UI
{
    public class RepositoryPageForm
    {
        public RepositoryPageForm()
        {
            AuthorRows = new List<AuthorRow>();
            CommitRows = new List<CommitRow>();
            CountCommandDate = DateTime.Today;
            AverageDateFrom = DateTime.Today;
            AverageDateTo = DateTime.Today;
            ReadOnlyMode = true;
        }


        private Repository _repository;
        
        [Context]
        public Session Session { get; set; }

        public string RepoPath { get; set; }

        public string InvalidRepoPathInfo { get; set; }

        public List<AuthorRow> AuthorRows { get; set; }

        public List<CommitRow> CommitRows { get; set; }

        public CommitRow FocusedCommitRow { get; set; }

        public AuthorRow FocusedAuthorRow { get; set; }

        public DateTime CountCommandDate { get; set; }

        public int CommitCount { get; set; }

        public bool ReadOnlyMode { get; set; }

        public DateTime AverageDateFrom { get; set; }
        public DateTime AverageDateTo { get; set; }
        public double AverageCommitCount { get; set; }

        public string InvalidRangeInfo { get; set; }

        public UIElement GetGitPanel()
        {
            var stack = new StackContainer();
            var group = new GroupContainer { CaptionHtml = "Git", LabelHeight = "10" };
            
            var gitPathRow = new RowContainer();
            var repoPathField = new FieldElement { CaptionHtml = "Ścieżka do repozytorium", EditValue = "{RepoPath}", OuterWidth = "100" };
            gitPathRow.Elements.Add(repoPathField);
            group.Elements.Add(gitPathRow);

            var commandRow = new RowContainer();
            var invalidPathLabel = new LabelElement { CaptionHtml = "{InvalidRepoPathInfo}"};
            var getDataCommand = new CommandElement { CaptionHtml = "Pobierz dane", MethodName = "GetGitData", Width = "20", };
            commandRow.Elements.Add(getDataCommand);
            commandRow.Elements.Add(invalidPathLabel);
            group.Elements.Add(commandRow);
            
            stack.Elements.Add(group);
            return stack;
        }

        public UIElement GetAuthorPanel()
        {
            var stack = new StackContainer();
            var group = new GroupContainer { CaptionHtml = "Autorzy", LabelHeight = "10" };

            var authorGrid = GridElement.CreatePopulateGrid(AuthorRows);
            authorGrid.EditValue = "{AuthorRows}";
            authorGrid.FocusedValue = "{FocusedAuthorRow}";
            authorGrid.Width = "100";

            group.Elements.Add(authorGrid);
            stack.Elements.Add(group);

            return stack;
        }

        public UIElement GetCommitPanel()
        {
            var stack = new StackContainer();
            var group = new GroupContainer { CaptionHtml = "Commity wybranego autora", LabelHeight = "10", IsReadOnly = "{ReadOnlyMode}" };

            var refreshCommandRow = new RowContainer();
            var refreshCommand = new CommandElement { CaptionHtml = "Odśwież", MethodName = "RefreshCommitList", Width = "20", };
            refreshCommandRow.Elements.Add(refreshCommand);
            group.Elements.Add(refreshCommandRow);

            var commitGrid = GridElement.CreatePopulateGrid(CommitRows);
            commitGrid.EditValue = "{CommitRows}";
            commitGrid.FocusedValue = "{FocusedCommitRow}";
            commitGrid.Width = "100";
            group.Elements.Add(commitGrid);

            stack.Elements.Add(group);
            return stack;
        }

        public UIElement GetStatisticPanel()
        {
            var stack = new StackContainer();
            var mainGroup = new GroupContainer { CaptionHtml = "Statystyki wybranego autora", LabelHeight = "10", IsReadOnly = "{ReadOnlyMode}" };

            var commitCountGroup = new GroupContainer { CaptionHtml = "Ilość commitów ", LabelHeight = "10" };

            var countCommandRow = new RowContainer();
            var dateField = new FieldElement { CaptionHtml = "Data", EditValue = "{CountCommandDate}", OuterWidth = "40" };
            countCommandRow.Elements.Add(dateField);
            commitCountGroup.Elements.Add(countCommandRow);

            var resultCountRow = new RowContainer();
            var commitCountField = new FieldElement { CaptionHtml = "Ilość", EditValue = "{CommitCount}", OuterWidth = "25", IsReadOnly = "true" };
            var countCommitsCommand = new CommandElement { CaptionHtml = "Pokaż wartość", MethodName = "SetCommitCount", OuterWidth = "15" };
            resultCountRow.Elements.Add(commitCountField);
            resultCountRow.Elements.Add(countCommitsCommand);
            commitCountGroup.Elements.Add(resultCountRow);

            mainGroup.Elements.Add(commitCountGroup);

            var averageCountGroup = new GroupContainer { CaptionHtml = "Średnia ilość commitów ", LabelHeight = "10" };

            var rangeRow = new RowContainer();
            var dateFromField = new FieldElement { CaptionHtml = "Od", EditValue = "{AverageDateFrom}", OuterWidth = "40" };
            var dateToField = new FieldElement { CaptionHtml = "Do", EditValue = "{AverageDateTo}", OuterWidth = "40" };
            rangeRow.Elements.Add(dateFromField);
            rangeRow.Elements.Add(dateToField);
            averageCountGroup.Elements.Add(rangeRow);

            var averageCountRow = new RowContainer();
            var averageCountField = new FieldElement { CaptionHtml = "Ilość", EditValue = "{AverageCommitCount}", OuterWidth = "25", IsReadOnly = "true" };
            var averageCountCommand = new CommandElement { CaptionHtml = "Pokaż wartość", MethodName = "SetAverageCommitCount", OuterWidth = "15" };
            var invalidRangeLabel = new LabelElement { CaptionHtml = "{InvalidRangeInfo}" };

            averageCountRow.Elements.Add(averageCountField);
            averageCountRow.Elements.Add(averageCountCommand);
            averageCountRow.Elements.Add(invalidRangeLabel);
            averageCountGroup.Elements.Add(averageCountRow);

            mainGroup.Elements.Add(averageCountGroup);

            stack.Elements.Add(mainGroup);
            return stack;
        }

        public void GetGitData()
        {
            _repository = new Repository(RepoPath);

            AuthorRows.Clear();
            CommitRows.Clear();

            if (_repository.HasProperAddress)
            {
                foreach (var author in _repository.Authors)
                    AuthorRows.Add(new AuthorRow { Name = author.Name, Email = author.Email });

                if(AuthorRows.Count > 0)
                {
                    string email = AuthorRows[0].Email;
                    foreach (var commit in _repository.Commits.FindAll(x=>x.Author.Email == email))
                        CommitRows.Add(new CommitRow { Message = commit.Message, DateTime = commit.DateTime.ToString() });
                }

                InvalidRepoPathInfo = string.Empty;
                ReadOnlyMode = false;
            }
            else
            {
                InvalidRepoPathInfo = "W podanej lokalizacji nie istnie repozytorium git";
                ReadOnlyMode = true;
            }

            Session.InvokeChanged();
        }

        public void RefreshCommitList()
        {
            CommitRows.Clear();
            foreach (var commit in _repository.Commits)
            {
                if(commit.Author.Email == FocusedAuthorRow.Email)
                    CommitRows.Add(new CommitRow { Message = commit.Message, DateTime = commit.DateTime.ToString() });
            }
            Session.InvokeChanged();
        }

        public void SetCommitCount()
        {
            var filteredCommits = _repository.Commits.FindAll(x => x.Author.Email == FocusedAuthorRow.Email && x.DateTime.Date == CountCommandDate.Date);
            CommitCount = filteredCommits.Count;
            Session.InvokeChanged();
        }

        public void SetAverageCommitCount()
        {
            var filteredCommits = _repository.Commits.FindAll(x => x.Author.Email == FocusedAuthorRow.Email &&
                                                                    AverageDateFrom.Date <= x.DateTime.Date &&
                                                                    x.DateTime.Date <= AverageDateTo.Date);


            int rangeDiff = (AverageDateTo.Date - AverageDateFrom).Days;
            if(rangeDiff >= 0)
            {
                int numberOfDaysInRage = rangeDiff + 1;
                AverageCommitCount = filteredCommits.Count / (double)numberOfDaysInRage;

                InvalidRangeInfo = string.Empty;
            }
            else
            {
                InvalidRangeInfo = "Nieprawidłowy zakres";
                AverageCommitCount = 0;
            }

            Session.InvokeChanged();
        }
    }
}
