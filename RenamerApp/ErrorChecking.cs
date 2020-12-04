namespace RenamerApp
{
    internal class ErrorChecking
    {
        public ErrorChecking(FileInputs fileInfo, WindowInputs windowInputs, ILogger logger)
        {
            FileInfo = fileInfo;
            WindowInputs = windowInputs;
            Logger = logger;
        }

        private FileInputs FileInfo { get; }
        private WindowInputs WindowInputs { get; }
        private ILogger Logger { get; }

        public bool DirectoryExistsOrNot()
        {
            if (FileInfo.CheckIfDirectoryExistsOrSetDefault() != "N/A") return true;
            Logger.Log("Directory does not exist - Please enter a valid output path or empty for default.");
            return false;
        }

        public bool FileExistsAndCopyEnabledAndDirectoryDefault()
        {
            if (!FileInfo.CheckIfFileExistsInOutput() || WindowInputs.OverwriteCheckBox != true ||
                WindowInputs.CopyCheckBox != true || FileInfo.OutputDirectory != FileInfo.Dire) return true;
            Logger.Log("File already exists - Can't overwrite a file already in use - Skipping file");
            WindowInputs.IncrementProgressBar();
            return false;
        }

        public bool FileExistsAndOverwriteNotChecked()
        {
            if (!FileInfo.CheckIfFileExistsInOutput() || WindowInputs.OverwriteCheckBox == true) return true;
            Logger.Log("File already exists - Overwrite not checked - Skipping file");
            WindowInputs.IncrementProgressBar();
            return false;
        }

        public void FileExistsAndOverwriteChecked()
        {
            if (FileInfo.CheckIfFileExistsInOutput() && WindowInputs.OverwriteCheckBox == true)
                Logger.Log("File already exists - Overwriting");
        }
    }
}