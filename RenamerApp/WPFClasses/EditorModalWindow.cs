using System.Windows;
using System.Windows.Controls;

namespace RenamerApp.WPFClasses
{
    internal class EditorModalWindow : Window
    {
        public EditorModalWindow(int width, int height)
        {
            ResizeMode = ResizeMode.NoResize;

            Width = width;
            Height = height;
            Content = Grid;
        }

        public Grid Grid { get; } = new Grid();
    }
}