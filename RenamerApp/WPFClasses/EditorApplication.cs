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
            Window.MinHeight = 300;
            Window.MinWidth = 516;
            Window.Height = 300;
            Window.Width = 516;
        }
    }
}
