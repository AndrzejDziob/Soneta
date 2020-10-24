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
            //RepoPath = @"D:\Programowanie\MySource\Soneta";
            Date = DateTime.Today;
            ShouldIncludeWeekendForStat = false;
        }

        private GridElement _commitGrid;
        private GridElement _authorGrid;
        
        [Context]
        public Session Session { get; set; }

        public string RepoPath { get; set; }

        public string InvalidRepoPathInfo { get; set; }

        public List<AuthorRow> AuthorRows { get; set; }

        public List<CommitRow> CommitRows { get; set; }

        public CommitRow FocusedCommitRow { get; set; }

        public AuthorRow FocusedAuthorRow { get; set; }

        public DateTime Date { get; set; }

        public bool ShouldIncludeWeekendForStat { get; set; }

        public UIElement GetGitPanel()
        {
            var stack = new StackContainer();
            var group = new GroupContainer { CaptionHtml = "Git", LabelHeight = "10" };
            var gitPathRow = new RowContainer();
            var commandRow = new RowContainer();

            var repoPathField = new FieldElement { CaptionHtml = "Ścieżka do repozytorium", EditValue = "{RepoPath}", OuterWidth = "100" };
            var invalidPathLabel = new LabelElement { CaptionHtml = "{InvalidRepoPathInfo}"};
            var getDataCommand = new CommandElement { CaptionHtml = "Pobierz dane", MethodName = "GetGitData", Width = "20", };
            var testCommand = new CommandElement { CaptionHtml = "Test", MethodName = "ShowMessageBox", Width = "20", };

            gitPathRow.Elements.Add(repoPathField);
            gitPathRow.Elements.Add(invalidPathLabel);

            commandRow.Elements.Add(getDataCommand);
            commandRow.Elements.Add(testCommand);

            group.Elements.Add(gitPathRow);
            group.Elements.Add(commandRow);
            
            stack.Elements.Add(group);
            return stack;
        }

        public UIElement GetAuthorPanel()
        {
            var stack = new StackContainer();
            var group = new GroupContainer { CaptionHtml = "Autorzy", LabelHeight = "10" };

            _authorGrid = GridElement.CreatePopulateGrid(AuthorRows);
            _authorGrid.EditValue = "{AuthorRows}";
            _authorGrid.FocusedValue = "{FocusedAuthorRow}";

            group.Elements.Add(_authorGrid);
            stack.Elements.Add(group);

            return stack;
        }

        public UIElement GetCommitPanel()
        {
            var stack = new StackContainer();
            var group = new GroupContainer { CaptionHtml = "Commity", LabelHeight = "10" };

            _commitGrid = GridElement.CreatePopulateGrid(CommitRows);
            _commitGrid.EditValue = "{CommitRows}";
            _commitGrid.FocusedValue = "{FocusedCommitRow}";

            group.Elements.Add(_commitGrid);
            stack.Elements.Add(group);

            return stack;
        }

        public UIElement GetCalendar()
        {
            var stack = new StackContainer();
            var group = new GroupContainer { CaptionHtml = "Data", LabelHeight = "10" };
            var row = new RowContainer();

            var dateField = new FieldElement { CaptionHtml = "Data", EditValue = "{Date}", OuterWidth = "100" };

            row.Elements.Add(dateField);

            group.Elements.Add(row);

            stack.Elements.Add(group);

            return stack;
        }

        public UIElement GetCheckBox()
        {
            var stack = new StackContainer();
            var group = new GroupContainer { CaptionHtml = "Bool", LabelHeight = "10" };
            var row = new RowContainer();

            var dateField = new FieldElement { CaptionHtml = "Czy uwzględniać weekendy", EditValue = "{ShouldIncludeWeekendForStat}", OuterWidth = "100" };

            row.Elements.Add(dateField);

            group.Elements.Add(row);

            stack.Elements.Add(group);

            return stack;
        }

        public void GetGitData()
        {
            var repository = new Repository(RepoPath);

            AuthorRows.Clear();
            CommitRows.Clear();

            if (repository.HasProperAddress)
            {
                foreach (var author in repository.Authors)
                    AuthorRows.Add(new AuthorRow { Name = author.Name, Email = author.Email });

                foreach (var commit in repository.Commits)
                    CommitRows.Add(new CommitRow { Message = commit.Message, DateTime = commit.DateTime.ToString() });

                InvalidRepoPathInfo = string.Empty;
            }
            else
            {
                //ShowMsgBoxInvalidRepoPath();
                InvalidRepoPathInfo = "W podanej lokalizacji nie istnie repozytorium git";
            }

            Session.InvokeChanged();
        }

        //public void ShowMsgBoxInvalidRepoPath()
        //{
        //    var msgBox = new MessageBoxInformation()
        //    {
        //        Text = "W podanej lokalizacji nie istnie repozytorium git"
        //    };

        //    msgBox.OKHandler.Invoke();
        //}


        public MessageBoxInformation ShowMessageBox()
        {
            return new MessageBoxInformation("Aktualne wartości")
            {
                Text = $"FocusedCommitRow - Msg: {FocusedCommitRow.Message}\nFocusedAuthorRow - Autor: {FocusedAuthorRow.Name}"
            };
        }
    }
}
