using System.Windows;
using System.Windows.Controls;

namespace RenamerApp.WPFClasses
{
    internal class EditorInformationList : ListBox
    {
        public EditorInformationList()
        {
            Height = 152;
            Width = 500;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            Margin = new Thickness(0, 110, 0, 0);
        }
    }
}