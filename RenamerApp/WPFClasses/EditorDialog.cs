using System.Windows;
using System.Windows.Controls;

namespace RenamerApp.WPFClasses
{
    class EditorDialog : Window
    {
        public Grid Grid { get; } = new Grid();
        public EditorDialog(int width, int height, int marginLeft, int marginTop)
        {
            Width = width;
            Height = height;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            Margin = new Thickness(marginLeft, marginTop, 0, 0);
        }
    }
}
