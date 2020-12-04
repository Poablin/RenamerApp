using System.Windows;
using System.Windows.Controls;

namespace RenamerApp.WPFClasses
{
    internal class EditorTextBlock : TextBlock
    {
        public EditorTextBlock(string text, int marginLeft, int marginTop)
        {
            //Width = width;
            //Height = height;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            Text = text;
            Margin = new Thickness(marginLeft, marginTop, 0, 0);
        }
    }
}