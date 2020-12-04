using System;

namespace RenamerApp
{
    internal interface ILogger
    {
        void Clear()
        {
        }

        void Log(string text)
        {
        }

        void Log(Exception ex)
        {
        }
    }
}