using System;
using System.Windows.Controls;

namespace RenamerApp
{
    internal class Logger : ILogger
    {
        public Logger(ItemCollection items)
        {
            Items = items;
        }

        private ItemCollection Items { get; }

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