using System.Windows;

namespace RenamerApp.WPFClasses
{
    class EditorApplication : Application
    {
        public EditorWindow Window { get; }
        public EditorApplication()
        {
            Window = new EditorWindow();
            Window.Title = "File Renamer";
            Window.Height = 300;
            Window.Width = 516;
            Window.MaxHeight = 300;
            Window.MaxWidth = 516;
        }
    }
}
