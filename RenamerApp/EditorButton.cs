using System.Windows;
using System.Windows.Controls;

namespace RenamerApp
{
    class EditorButton : Button
    {
        public EditorButton(int marginLeft, int marginTop, string content)
        {
            Content = content;
            Width = 50;
            Height = 50;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            Margin = new Thickness(marginLeft,marginTop,0,0);
        }
    }
}
