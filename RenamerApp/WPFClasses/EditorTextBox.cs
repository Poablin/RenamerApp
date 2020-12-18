using Gu.Wpf.Adorners;
using System.Windows;
using System.Windows.Controls;

namespace RenamerApp.WPFClasses
{
    internal class EditorTextBox : TextBox
    {
        public EditorTextBox(string watermark, int maxLength, int width, int marginLeft, int marginTop)
        {
            MaxLength = maxLength;
            this.SetText(watermark);
            Width = width;
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Top;
            Margin = new Thickness(marginLeft, marginTop, 0, 0);
        }
    }
}