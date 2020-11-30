using System.Windows;
using System.Windows.Controls;

namespace RenamerApp.WPFClasses
{
    internal class EditorWindow : Window
    {
        private Grid Grid { get; } = new Grid();
        public ListBox InformationList { get; }
        public TextBox OutputDirectoryInputBox { get; }        
        public TextBox ReplaceSpecificCharInputBox { get; }
        public TextBox ReplaceSpecificCharOutputBox { get; }
        public EditorCheckBox CopyCheckBox { get; }
        public EditorCheckBox UpperCaseCheckBox { get; }
        public EditorCheckBox TrimCheckBox { get; }
        public EditorButton StartButton { get; }
        public EditorButton SelectFilesButton { get; }
        public EditorButton SelectOutputButton { get; }
        public EditorProgressBar ProgressBar { get; }
        private ContextMenu Context { get; } = new ContextMenu();
        public MenuItem ContextItem1 { get; } = new MenuItem { Header = "Copy" };

        public EditorWindow()
        {
            Context.Items.Add(ContextItem1);

            Grid.Children.Add(InformationList = new EditorInformationList() { ContextMenu = Context });
            Grid.Children.Add(OutputDirectoryInputBox = new EditorTextBox("Output Path", 200, 150, 20));
            Grid.Children.Add(ReplaceSpecificCharInputBox = new EditorTextBox("", 15, 0, 50));
            Grid.Children.Add(ReplaceSpecificCharOutputBox = new EditorTextBox("", 15, 15, 50));
            Grid.Children.Add(CopyCheckBox = new EditorCheckBox(200, 20, 350, 10, "Copy"));
            Grid.Children.Add(UpperCaseCheckBox = new EditorCheckBox(200, 20, 350, 40, "Uppercase"));
            Grid.Children.Add(TrimCheckBox = new EditorCheckBox(200, 20, 350, 70, "Trim"));
            Grid.Children.Add(StartButton = new EditorButton(0, 0, "Start"));
            Grid.Children.Add(SelectFilesButton = new EditorButton(50, 0, "Select"));
            Grid.Children.Add(SelectOutputButton = new EditorButton(100, 0, "Output"));
            Grid.Children.Add(ProgressBar = new EditorProgressBar(500, 20, 0, 90));

            Content = Grid;
        }
    }
}
