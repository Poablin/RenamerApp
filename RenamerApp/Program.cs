using RenamerApp.WPF_Classes;
using System;

namespace RenamerApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var app = new EditorApplication();
            var operations = new Operations(app.Window);
            app.Run(app.Window);
        }
    }
}
