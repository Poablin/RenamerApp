using RenamerApp.WPFClasses;

namespace RenamerApp
{
    class WindowInputs
    {
        readonly EditorWindow Window;
        public string SpecificStringThis { get; }
        public string SpecificStringWith { get; }
        public string FromIndex { get; }
        public string ToIndex { get; }
        public bool? TrimCheckBox { get; }
        public bool? UppercaseCheckBox { get; }
        public WindowInputs(EditorWindow window)
        {
            Window = window;
            SpecificStringThis = Window.SpecificStringReplaceThisInputBox.Text;
            SpecificStringWith = Window.SpecificStringReplaceWithInputBox.Text;
            FromIndex = Window.FromIndexInputBox.Text;
            ToIndex = Window.ToIndexInputBox.Text;
            TrimCheckBox = Window.TrimCheckBox.IsChecked;
            UppercaseCheckBox = Window.UpperCaseCheckBox.IsChecked;
        }
    }
}
