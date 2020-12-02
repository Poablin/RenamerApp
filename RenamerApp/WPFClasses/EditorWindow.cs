using System.Windows;
using System.Windows.Controls;

namespace RenamerApp.WPFClasses
{
    internal class EditorWindow : Window
    {
        private Grid Grid { get; } = new Grid();
        public ListBox InformationList { get; }
        public TextBox OutputDirectoryInputBox { get; }
        public TextBox SpecificStringReplaceThisInputBox { get; }
        public TextBox SpecificStringReplaceWithInputBox { get; }
        public TextBox FromIndexInputBox { get; }
        public TextBox ToIndexInputBox { get; }
        public EditorCheckBox CopyCheckBox { get; }
        public EditorCheckBox UpperCaseCheckBox { get; }
        public EditorCheckBox TrimCheckBox { get; }
        public EditorButton StartButton { get; }
        public EditorButton SelectFilesButton { get; }
        public EditorButton SelectOutputButton { get; }
        public EditorButton ResetUiButton { get; }
        public EditorProgressBar ProgressBar { get; }
        private ContextMenu Context { get; } = new ContextMenu();
        public MenuItem ContextItem1 { get; } = new MenuItem { Header = "Copy Text" };
        public EditorTextBlock SelectedFilesText { get; }

        public EditorWindow()
        {
            Context.Items.Add(ContextItem1);

            Grid.Children.Add(InformationList = new EditorInformationList() { ContextMenu = Context });
            Grid.Children.Add(OutputDirectoryInputBox = new EditorTextBox("Output Path", 0, 200, 150, 20));
            Grid.Children.Add(SpecificStringReplaceThisInputBox = new EditorTextBox("", 0, 50, 80, 50));
            Grid.Children.Add(SpecificStringReplaceWithInputBox = new EditorTextBox("", 0, 50, 162, 50));
            Grid.Children.Add(FromIndexInputBox = new EditorTextBox("", 2, 20, 150, 70));
            Grid.Children.Add(ToIndexInputBox = new EditorTextBox("", 2, 20, 190, 70));
            Grid.Children.Add(CopyCheckBox = new EditorCheckBox(50, 20, 370, 0, "Copy files"));
            Grid.Children.Add(UpperCaseCheckBox = new EditorCheckBox(75, 20, 370, 20, "Uppercase") { IsChecked = true });
            Grid.Children.Add(TrimCheckBox = new EditorCheckBox(45, 20, 370, 40, "Trim"));
            Grid.Children.Add(StartButton = new EditorButton(0, 0, "Start"));
            Grid.Children.Add(SelectFilesButton = new EditorButton(50, 0, "Select"));
            Grid.Children.Add(SelectOutputButton = new EditorButton(100, 0, "Output"));
            Grid.Children.Add(ResetUiButton = new EditorButton(450, 40, "Reset UI"));
            Grid.Children.Add(ProgressBar = new EditorProgressBar(500, 20, 0, 90));
            Grid.Children.Add(SelectedFilesText = new EditorTextBlock("", 160, 2));

            var text1 = new EditorTextBlock("Replace string", 0, 50);
            var text2 = new EditorTextBlock("with", 135, 50);
            Grid.Children.Add(text1);
            Grid.Children.Add(text2);

            var text3 = new EditorTextBlock("Keep everything from index", 0, 70);
            var text4 = new EditorTextBlock("to", 175, 70);
            Grid.Children.Add(text3);
            Grid.Children.Add(text4);


            Content = Grid;
        }
    }
}
