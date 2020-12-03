using System.Windows.Controls;

namespace RenamerApp.WPFClasses
{
    class EditorContextMenu : ContextMenu
    {
        public MenuItem ContextItem1 { get; } = new MenuItem { Header = "Copy Text" };
        public EditorContextMenu()
        {
            Items.Add(ContextItem1);
        }
    }
}
