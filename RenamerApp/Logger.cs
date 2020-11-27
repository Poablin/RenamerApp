using System;
using System.Windows.Controls;

namespace RenamerApp
{
    class Logger : ILogger
    {
        private ItemCollection Items { get; }
        public Logger(ItemCollection items)
        {
            Items = items;
        }
        public void Clear()
        {
            Items.Clear();
        }
        public void Log(string s)
        {
            Items.Add(s);
        }
        public void Log(Exception e)
        {
            Items.Add(e);
        }
    }
}
