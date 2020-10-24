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
            RepoPath = @"D:\Programowanie\MySource\Soneta";
        }

        private GridElement _commitGrid;
        private GridElement _authorGrid;
        
        [Context]
        public Session Session { get; set; }

        //[Context]
        //public Login Login { get; set; }

        public string RepoPath { get; set; }

        public string Info { get; set; }

        public List<AuthorRow> AuthorRows { get; set; }

        public List<CommitRow> CommitRows { get; set; }

        public CommitRow FocusedCommitRow { get; set; }

        public AuthorRow FocusedAuthorRow { get; set; }


        public UIElement GetAuthorsUI()
        {
            var stack = new StackContainer();
            var group = new GroupContainer { CaptionHtml = "Autorzy", LabelHeight = "10" };
            var row = new RowContainer();
            var rowCmd = new RowContainer();

            var fieldRepoPath = new FieldElement { CaptionHtml = "Ścieżka do repozytorium git", EditValue = "{RepoPath}", OuterWidth = "100" };

            var command = new CommandElement { CaptionHtml = "Pobierz dane", MethodName = "GetGitData", Width = "20", };

            var commandGridProperty = new CommandElement { CaptionHtml = "Test", MethodName = "ShowMessageBox", Width = "20", };

            row.Elements.Add(fieldRepoPath);
            rowCmd.Elements.Add(command);
            rowCmd.Elements.Add(commandGridProperty);

            group.Elements.Add(row);
            group.Elements.Add(rowCmd);

            group.Elements.Add(GetAuthorGrid());

            stack.Elements.Add(group);

            return stack;
        }

        public UIElement GetAuthorGrid()
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

        public UIElement GetCommitGrid()
        {
            var stack = new StackContainer();
            var group = new GroupContainer { CaptionHtml = "Commit", LabelHeight = "10" };

            _commitGrid = GridElement.CreatePopulateGrid(CommitRows);
            _commitGrid.EditValue = "{CommitRows}";
            _commitGrid.FocusedValue = "{FocusedCommitRow}";

            var infoField = new FieldElement { CaptionHtml = "Commit", EditValue = "{Info}", OuterWidth = "100" };

            group.Elements.Add(_commitGrid);
            group.Elements.Add(infoField);
            stack.Elements.Add(group);

            return stack;
        }

        public void GetGitData()
        {
            AuthorRows.Clear();
            CommitRows.Clear();

            var repository = new Repository(RepoPath);

            foreach (var author in repository.Authors)
                AuthorRows.Add(new AuthorRow { Name = author.Name, Email = author.Email });

            foreach (var commit in repository.Commits)
                CommitRows.Add(new CommitRow { Message = commit.Message, DateTime = commit.DateTime.ToString() });
                
            Session.InvokeChanged();
        }


        public MessageBoxInformation ShowMessageBox()
        {
            return new MessageBoxInformation("Aktualne wartości")
            {
                Text = $"FocusedCommitRow - Msg: {FocusedCommitRow.Message}\nFocusedAuthorRow - Autor: {FocusedAuthorRow.Name}"
            };
        }
    }
}
