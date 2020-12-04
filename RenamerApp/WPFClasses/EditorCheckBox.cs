using System.Windows;
using System.Windows.Controls;

namespace RenamerApp.WPFClasses
{
    internal class EditorCheckBox : CheckBox
    {
        public EditorCheckBox(int marginLeft, int marginTop, string content)
        {
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            Margin = new Thickness(marginLeft, marginTop, 0, 0);
            //FlowDirection = FlowDirection.RightToLeft;
            Content = content;
        }
    }
}