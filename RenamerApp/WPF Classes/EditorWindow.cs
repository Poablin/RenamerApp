using System.Windows;
using System.Windows.Controls;

namespace RenamerApp
{
    internal class EditorWindow : Window
    {
        public ListBox InformationList { get; }
        public TextBox OutputDirectoryInputBox { get; }
        public CheckBox CopyCheckBox { get; }
        public CheckBox UpperCaseCheckBox { get; }
        public CheckBox TrimCheckBox { get; }
        public EditorButton StartButton { get; }
        public EditorButton SelectFilesButton { get; }
        public EditorButton SelectOutputButton { get; }
        public ContextMenu C { get; }
        public MenuItem I1 { get; }
        public string[] FilePaths { get; set; }

        public EditorWindow()
        {
            var grid = new Grid();
            var context = new ContextMenu();
            InformationList = new EditorInformationList();
            OutputDirectoryInputBox = new EditorTextBox("Output Path", 200, 100, 70);
            CopyCheckBox = new EditorCheckBox(200, 20, 350, 10, "Copy");
            UpperCaseCheckBox = new EditorCheckBox(200, 20, 350, 40, "Uppercase");
            TrimCheckBox = new EditorCheckBox(200, 20, 350, 70, "Trim");
            StartButton = new EditorButton(0, 0, "Start");
            SelectFilesButton = new EditorButton(0, 50, "Select");
            SelectOutputButton = new EditorButton(50, 50, "Output");

            C = new ContextMenu();
            I1 = new MenuItem {Header = "Copy"};
            C.Items.Add(I1);
            InformationList.ContextMenu = C;

            grid.Children.Add(InformationList);
            grid.Children.Add(CopyCheckBox);
            grid.Children.Add(UpperCaseCheckBox);
            grid.Children.Add(TrimCheckBox);
            grid.Children.Add(OutputDirectoryInputBox);
            grid.Children.Add(StartButton);
            grid.Children.Add(SelectFilesButton);
            grid.Children.Add(SelectOutputButton);

            Content = grid;
        }
    }
}
