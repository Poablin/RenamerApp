using System.Windows;
using System.Windows.Controls;

namespace RenamerApp.WPFClasses
{
    class EditorModalWindow : Window
    {
        public Grid Grid { get; } = new Grid();
        public EditorModalWindow(int width, int height)
        {
            Width = width;
            Height = height;
            MinWidth = width;
            MinHeight = height;
            Content = Grid;
        }
    }
}
