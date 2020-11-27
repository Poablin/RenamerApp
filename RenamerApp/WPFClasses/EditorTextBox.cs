using System.Windows;
using System.Windows.Controls;
using Gu.Wpf.Adorners;

namespace RenamerApp.WPFClasses
{
    class EditorTextBox : TextBox
    {
        public EditorTextBox(string watermark, int width, int marginLeft, int marginTop)
        {
            Watermark.SetText(this, watermark);
            Width = width;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            Margin = new Thickness(marginLeft, marginTop, 0, 0);
        }
    }
}
