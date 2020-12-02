using RenamerApp.WPFClasses;

namespace RenamerApp
{
    class WindowInputs
    {
        readonly EditorWindow Window;
        public string OutputDirectory { get; }
        public string SpecificStringThis { get; }
        public string SpecificStringWith { get; }
        public string FromIndex { get; }
        public string ToIndex { get; }
        public bool? TrimCheckBox { get; }
        public bool? UppercaseCheckBox { get; }
        public bool? CopyCheckBox { get; }
        public bool? OverwriteCheckBox { get; }
        public WindowInputs(EditorWindow window)
        {
            Window = window;
            OutputDirectory = Window.OutputDirectoryInputBox.Text;
            SpecificStringThis = Window.SpecificStringReplaceThisInputBox.Text;
            SpecificStringWith = Window.SpecificStringReplaceWithInputBox.Text;
            FromIndex = Window.FromIndexInputBox.Text;
            ToIndex = Window.ToIndexInputBox.Text;
            TrimCheckBox = Window.TrimCheckBox.IsChecked;
            UppercaseCheckBox = Window.UpperCaseCheckBox.IsChecked;
            CopyCheckBox = Window.CopyCheckBox.IsChecked;
            OverwriteCheckBox = Window.OverwriteCheckBox.IsChecked;
        }
    }
}
