using System.Windows;
using System.Windows.Controls;

namespace RenamerApp.WPFClasses
{
    internal class EditorWindow : Window
    {
        private Grid Grid { get; } = new Grid();
        public ListBox InformationList { get; }
        public TextBox OutputDirectoryInputBox { get; }
        public TextBox SpecificCharReplaceInputBox { get; }
        public TextBox SpecificCharReplaceOutputBox { get; }
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
            Grid.Children.Add(OutputDirectoryInputBox = new EditorTextBox("Output Path", 0, 200, 150, 20));
            Grid.Children.Add(SpecificCharReplaceInputBox = new EditorTextBox("", 1, 15, 72, 50));
            Grid.Children.Add(SpecificCharReplaceOutputBox = new EditorTextBox("", 1, 15, 120, 50));
            Grid.Children.Add(CopyCheckBox = new EditorCheckBox(200, 20, 350, 10, "Copy"));
            Grid.Children.Add(UpperCaseCheckBox = new EditorCheckBox(200, 20, 350, 40, "Uppercase"));
            Grid.Children.Add(TrimCheckBox = new EditorCheckBox(200, 20, 350, 70, "Trim"));
            Grid.Children.Add(StartButton = new EditorButton(0, 0, "Start"));
            Grid.Children.Add(SelectFilesButton = new EditorButton(50, 0, "Select"));
            Grid.Children.Add(SelectOutputButton = new EditorButton(100, 0, "Output"));
            Grid.Children.Add(ProgressBar = new EditorProgressBar(500, 20, 0, 90));

            var text1 = new EditorTextBlock("Replace Char", 0, 50);
            var text2 = new EditorTextBlock("With", 90, 50);
            Grid.Children.Add(text1);
            Grid.Children.Add(text2);

            Content = Grid;
        }
    }
}
