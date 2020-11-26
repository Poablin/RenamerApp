using System;
using System.Windows;

namespace RenamerApp
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var app = new Application();
            var window = new EditorWindow();
            window.Title = "File Renamer";
            window.MinHeight = 300;
            window.MinWidth = 500;
            window.Height = 300;
            window.Width = 516;
            app.Run(window);
        }
    }
}
