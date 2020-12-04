using System.Windows;

namespace RenamerApp.WPFClasses
{
    internal class EditorApplication : Application
    {
        public EditorApplication()
        {
            Window = new EditorWindow();
            Window.Title = "FileRenamer";
            Window.Height = 300;
            Window.Width = 516;
            Window.MaxHeight = 300;
            Window.MaxWidth = 516;
        }

        public EditorWindow Window { get; }
    }
}