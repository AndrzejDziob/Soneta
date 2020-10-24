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

            //Row = string.Empty;
            FocusedValue = null;
            //FocusedColumnValue = string.Empty;
            //Generator_Info = string.Empty;
            //SelectedValue = null;
            //Tag = string.Empty;
            //TagInfo = string.Empty;
            //TagObject = string.Empty;
            //TreeNodesValue = string.Empty;
            //Column = string.Empty;
            //DataContext = string.Empty;

        }

        private GridElement _commitGrid;
        
        [Context]
        public Session Session { get; set; }

        //[Context]
        //public Login Login { get; set; }

        public string RepoPath { get; set; }

        public string Info { get; set; }

        public List<AuthorRow> AuthorRows { get; set; }

        public List<CommitRow> CommitRows { get; set; }


        //public string Row { get; set; }
        public CommitRow FocusedValue { get; set; }
        //public string FocusedColumnValue { get; set; }
        //public string Generator_Info { get; set; }
        //public CommitRow SelectedValue { get; set; }
        //public string Tag { get; set; }
        //public string TagInfo { get; set; }
        //public string TagObject { get; set; }
        //public string TreeNodesValue { get; set; }
        //public string Column { get; set; }
        //public string DataContext { get; set; }


        public UIElement GetAuthorsUI()
        {
            var stack = new StackContainer();
            var group = new GroupContainer { CaptionHtml = "Autorzy", LabelHeight = "10" };
            var row = new RowContainer();
            var rowCmd = new RowContainer();

            var fieldRepoPath = new FieldElement { CaptionHtml = "Ścieżka do repozytorium git", EditValue = "{RepoPath}", OuterWidth = "100" };

            var command = new CommandElement { CaptionHtml = "Pobierz dane", MethodName = "GetGitData", Width = "20", };

            var commandGridProperty = new CommandElement { CaptionHtml = "Test", MethodName = "ShowFieldValue", Width = "20", };


            row.Elements.Add(fieldRepoPath);
            rowCmd.Elements.Add(command);
            rowCmd.Elements.Add(commandGridProperty);

            group.Elements.Add(row);
            group.Elements.Add(rowCmd);

            stack.Elements.Add(group);

            return stack;
        }

        public UIElement GetCommitGridUI()
        {
            var stack = new StackContainer();
            var group = new GroupContainer { CaptionHtml = "Commit", LabelHeight = "10" };

            _commitGrid = GridElement.CreatePopulateGrid(CommitRows);
            _commitGrid.EditValue = "{CommitRows}";

            //_commitGrid.Row = "{Row}";
            _commitGrid.FocusedValue = "{FocusedValue}";
            //_commitGrid.FocusedColumnValue = "{FocusedColumnValue}";
            //_commitGrid.Generator_Info = "{Generator_Info}";
            //_commitGrid.SelectedValue = "{SelectedValue}";
            //_commitGrid.Tag = "{Tag}";
            //_commitGrid.TagInfo = "{TagInfo}";
            //_commitGrid.TagObject = "{TagObject}";
            //_commitGrid.TreeNodesValue = "{TreeNodesValue}";
            //_commitGrid.Column = "{Column}";
            //_commitGrid.DataContext = "{DataContext}";


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


        public MessageBoxInformation ShowFieldValue()
        {
            return new MessageBoxInformation("Aktualne wartości")
            {
                Text = $"FocusedValue: Msg: {FocusedValue.Message}"
            };
        }
    }
}
