using RenamerApp.WPFClasses;
using System;

namespace RenamerApp
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var app = new EditorApplication();
            var logger = new Logger(app.Window.InformationList.Items);
            var operations = new Operations(app.Window, logger);
            app.Run(app.Window);
        }
    }
}