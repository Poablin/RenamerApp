using System;
using System.IO;
using System.Linq;

namespace RenamerApp
{
    public class FileInfo
    {
        public string File { get; }
        public string Dire { get; }
        public string Name { get; internal set; }
        public string Exte { get; }
        public string Oldn { get; }
        public bool Copy { get; set; }
        
        public string OutputDirectory { get; internal set; }
        public string LogStartProcessing => $"Processing: \"{Oldn}{Exte}\"";
        public string LogFinishedProcessing
        {
            get
            {
                string str = string.Empty;
                if (Oldn != Name) str += $"Renamed \"{Oldn}\" ";
                if (Copy == true) str += $"Copied \"{Name}{Exte}\" ";
                if (OutputDirectory != Dire && Dire != "") str += $"Moved \"{Name}{Exte}\" to {OutputDirectory} ";
                return str.Trim();
            }
        }
        public FileInfo(string file)
        {
            File = file;
            Dire = Path.GetDirectoryName(file);
            Name = Path.GetFileNameWithoutExtension(file);
            Exte = Path.GetExtension(file);
            Oldn = Path.GetFileNameWithoutExtension(file);
        }
    }
}