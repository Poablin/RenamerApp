using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Threading.Tasks;
using RenamerApp.WPFClasses;

namespace RenamerApp
{
    class Operations
    {
        private ILogger Logger { get; }
        private EditorWindow Window { get; }
        private string[] FilePaths { get; set; }
        public Operations(EditorWindow window, ILogger logger)
        {
            Logger = logger;
            Window = window;
            Window.StartButton.Click += StartOperation;
            Window.SelectFilesButton.Click += SelectFiles;
            Window.SelectOutputButton.Click += SelectOutputFolder;
            Window.ContextItem1.Click += ContextItem1_Click;
            Window.ResetUiButton.Click += ResetUi;
        }
        private async void StartOperation(object sender, RoutedEventArgs e)
        {
            var windowInputs = new WindowInputs(Window);
            Window.ProgressBar.Value = 0;
            Logger.Clear();
            if (FilePaths == null)
            {
                Logger.Log("No files selected");
                return;
            }
            Logger.Log("Starting operation - please wait");
            try
            {
                Window.ProgressBar.Maximum = FilePaths.Length;
                foreach (string file in FilePaths)
                {
                    var fileInfo = new FileInfo(file) { Copy = windowInputs.CopyCheckBox, OutputDirectory = windowInputs.OutputDirectory };
                    var fileNameEditor = new FileNameEditor(fileInfo);
                    //Under kan endres hva som skjer med navnet
                    if (windowInputs.FromIndex != "") fileNameEditor.DeleteEverythingElse(windowInputs.FromIndex, windowInputs.ToIndex);
                    if (windowInputs.SpecificStringThis != "") fileNameEditor.ReplaceSpecificString(windowInputs.SpecificStringThis, windowInputs.SpecificStringWith);
                    if (windowInputs.TrimCheckBox == true) fileNameEditor.Trim();
                    fileNameEditor.UpperCase(windowInputs.UppercaseCheckBox);
                    Logger.Log(fileInfo.LogStartProcessing);
                    //Forskjellig error checking
                    fileInfo.CheckIfDirectoryExistsOrSetDefault();
                    if (fileInfo.CheckIfFileExistsInOutput() && windowInputs.OverwriteCheckBox != true && windowInputs.CopyCheckBox == true)
                    {
                        Logger.Log("File already exists - Overwrite not checked - Skipping file");
                        Window.ProgressBar.Value++;
                        continue;
                    }
                    if (fileInfo.CheckIfFileExistsInOutput() && windowInputs.OverwriteCheckBox == true && windowInputs.CopyCheckBox == true && fileInfo.OutputDirectory == fileInfo.Dire)
                    {
                        Logger.Log("File already exists - Can't overwrite a file already in use - Skipping file");
                        Window.ProgressBar.Value++;
                        continue;
                    }
                    if (fileInfo.CheckIfFileExistsInOutput() && windowInputs.OverwriteCheckBox != true)
                    {
                        Logger.Log("File already exists - Overwrite not checked - Skipping file");
                        Window.ProgressBar.Value++;
                        continue;

                    }
                    //Output ting her nede
                    if (fileInfo.CheckIfFileExistsInOutput() && windowInputs.OverwriteCheckBox == true) Logger.Log("File already exists - overwriting");
                    await Task.Run(() => CopyOrMoveFiles(fileInfo.OutputDirectory, fileInfo, windowInputs.CopyCheckBox, (bool)windowInputs.OverwriteCheckBox));
                    Window.ProgressBar.Value++;
                    Logger.Log(fileInfo.LogFinishedProcessing);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            finally
            {
                Logger.Log("Operation finished!");
                FilePaths = null;
                Window.SelectedFilesText.Text = "";
                Window.InformationList.ScrollIntoView(Window.InformationList.Items[Window.InformationList.Items.Count - 1]);
            }
        }
        private void CopyOrMoveFiles(string outputDirectory, FileInfo fileInfo, bool? copy, bool overwrite)
        {
            if (copy == true) File.Copy($"{fileInfo.FullFile}", $"{(outputDirectory == "" ? fileInfo.Dire : outputDirectory)}\\{fileInfo.Name}{fileInfo.Exte}", overwrite);
            else File.Move($"{fileInfo.FullFile}", $"{(outputDirectory == "" ? fileInfo.Dire : outputDirectory)}\\{fileInfo.Name}{fileInfo.Exte}", overwrite);
        }
        private void SelectFiles(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Multiselect = true };
            if (openFileDialog.ShowDialog() != true) return;
            FilePaths = openFileDialog.FileNames;
            Window.SelectedFilesText.Text = $"Selected {FilePaths.Length} {(FilePaths.Length < 2 ? "File" : "Files")}";
        }
        private void SelectOutputFolder(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog { IsFolderPicker = true };
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result != CommonFileDialogResult.Ok) return;
            Window.OutputDirectoryInputBox.Text = dialog.FileName;
        }
        private void ContextItem1_Click(object sender, RoutedEventArgs e)
        {
            if (Window.InformationList.SelectedItem == null) return;
            Clipboard.SetText(Window.InformationList.SelectedItem.ToString() ?? throw new InvalidOperationException());
        }
        private void ResetUi(object sender, RoutedEventArgs e)
        {
            Logger.Clear();
            FilePaths = null;
            Window.SelectedFilesText.Text = "";
            Window.ProgressBar.Value = 0;
            Window.OutputDirectoryInputBox.Text = "";
            Window.FromIndexInputBox.Text = "";
            Window.ToIndexInputBox.Text = "";
            Window.SpecificStringReplaceThisInputBox.Text = "";
            Window.SpecificStringReplaceWithInputBox.Text = "";
            Window.UpperCaseCheckBox.IsChecked = true;
            Window.TrimCheckBox.IsChecked = true;
            Window.CopyCheckBox.IsChecked = false;
            Window.OverwriteCheckBox.IsChecked = false;
        }
    }
}
