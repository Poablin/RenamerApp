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
        public void ReplaceSpecificString(string firststring, string secondstring)
        {
            FileInfo.Name = FileInfo.Name.Replace(firststring, secondstring);
        }
        public void DeleteEverythingElse(string fromIndex, string toIndex)
        {
            if (toIndex == "") FileInfo.Name = FileInfo.Name.Substring(Convert.ToInt32(fromIndex));
            else FileInfo.Name = FileInfo.Name.Substring(Convert.ToInt32(fromIndex), Convert.ToInt32(toIndex));
        }
    }
}
