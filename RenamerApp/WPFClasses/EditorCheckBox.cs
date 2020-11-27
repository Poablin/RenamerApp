using System.Windows;
using System.Windows.Controls;

namespace RenamerApp.WPFClasses
{
    class EditorCheckBox : CheckBox
    {

        public EditorCheckBox(int width, int height, int marginLeft, int marginTop, string content)
        {
            Width = width;
            Height = height;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            Margin = new Thickness(marginLeft, marginTop, 0, 0);
            Content = content;
        }
    }
}
