using System;
using RenamerApp.WPFClasses;

namespace RenamerApp
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var app = new EditorApplication();
            var operations = new Operations(app.Window);
            app.Run(app.Window);
        }
    }
}
