using System;

namespace RenamerApp
{
    interface ILogger
    {
        void Clear() { }
        void Log(string text) { }
        void Log(Exception ex) { }
    }
}
