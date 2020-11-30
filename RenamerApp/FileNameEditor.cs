using System;

namespace RenamerApp
{
    class FileNameEditor
    {
        private FileInfo FileInfo;
        public FileNameEditor(FileInfo file)
        {
            FileInfo = file;
        }
        public void Trim()
        {
            FileInfo.Name.Trim();
        }
        public void UpperCase(bool? isChecked)
        {
            FileInfo.Name = isChecked == true ? FileInfo.Name.Substring(0, 1).ToUpper() + FileInfo.Name[1..] : FileInfo.Name.Substring(0, 1).ToLower() + FileInfo.Name[1..];
        }
        public void ReplaceSpecificChar()
        {
            //name = name.Replace("_", " ");
            //name = name.Replace("  ", " ");
        }
        public void DeleteFrom()
        {
            //name = name.Substring(6);
            //name = name.Substring(6, 8);
        }
        public void RemoveSpaces()
        {
            //FileInfo.Split();
        }
    }
}
