using System.Windows;
using System.Windows.Controls;

namespace RenamerApp.WPFClasses
{
    internal class EditorWindow : Window
    {
        private Grid Grid { get; } = new Grid();
        public ListBox InformationList { get; }
        public TextBox OutputDirectoryInputBox { get; }
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

            Grid.Children.Add(InformationList = new EditorInformationList { ContextMenu = Context });
            Grid.Children.Add(OutputDirectoryInputBox = new EditorTextBox("Output Path", 200, 100, 70));
            Grid.Children.Add(CopyCheckBox = new EditorCheckBox(200, 20, 350, 10, "Copy"));
            Grid.Children.Add(UpperCaseCheckBox = new EditorCheckBox(200, 20, 350, 40, "Uppercase"));
            Grid.Children.Add(TrimCheckBox = new EditorCheckBox(200, 20, 350, 70, "Trim"));
            Grid.Children.Add(StartButton = new EditorButton(0, 0, "Start"));
            Grid.Children.Add(SelectFilesButton = new EditorButton(0, 50, "Select"));
            Grid.Children.Add(SelectOutputButton = new EditorButton(50, 50, "Output"));
            Grid.Children.Add(ProgressBar = new EditorProgressBar(200, 20, 100, 90));

            Content = Grid;
        }
    }
}
