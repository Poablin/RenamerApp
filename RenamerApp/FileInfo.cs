using System.IO;

namespace RenamerApp
{
    public class FileInfo
    {
        public string File { get; }
        public string Dire { get; }
        public string Name { get; private set; }
        public string Exte { get; }
        public string Oldn { get; }
        public bool Copy { get; set; }
        public string LogStartProcessing => $"Processing: \"{Name}{Exte}\"";
        public string LogFinishedProcessing
        {
            get
            {
                string str = Oldn != Name ? $"Renamed \"{Oldn}\" to \"{Name}{Exte}\"" : Copy == true ? $"Copied \"{Name}{Exte}\" to {Dire}" : $"Moved \"{Name}{Exte}\" to {Dire}";
                return str;
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
        public void Trim()
        {
            File.Trim();
        }
        public void UpperCase(bool? IsChecked)
        {
            Name = IsChecked == true ? Name.Substring(0, 1).ToUpper() + Name[1..] : Name.Substring(0, 1).ToLower() + Name[1..];
        }

    }
}