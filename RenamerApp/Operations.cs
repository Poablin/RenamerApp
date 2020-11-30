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
        }
        private async void StartOperation(object sender, RoutedEventArgs e)
        {
            var outputDirectory = Window.OutputDirectoryInputBox.Text;
            bool copy = false;
            Logger.Clear();
            if (Window.CopyCheckBox.IsChecked == true) copy = true;
            if (FilePaths == null)
            {
                Logger.Log("No files selected");
                return;
            }
            Logger.Log("Started operation... please wait");
            try
            {
                Window.ProgressBar.Maximum = FilePaths.Length;
                foreach (string file in FilePaths)
                {
                    var fileInfo = new FileInfo(file) { Copy = copy, OutputDirectory = outputDirectory };
                    var fileNameEditor = new FileNameEditor(fileInfo);
                    var windowInputs = new WindowInputs(Window);
                    //Under kan endres hva som skjer med navnet
                    if (windowInputs.FromIndex != "") fileNameEditor.DeleteEverythingElse(windowInputs.FromIndex, windowInputs.ToIndex);
                    if (windowInputs.SpecificStringThis != "") fileNameEditor.ReplaceSpecificString(windowInputs.SpecificStringThis, windowInputs.SpecificStringWith);
                    if (windowInputs.TrimCheckBox == true) fileNameEditor.Trim();
                    fileNameEditor.UpperCase(windowInputs.UppercaseCheckBox);
                    //Her bestemmer man hvor det skal outputtes til
                    Logger.Log(fileInfo.LogStartProcessing);
                    await Task.Run(() => CopyOrMoveFiles(outputDirectory, fileInfo, copy));
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
                Window.SelectFilesButton.Content = "Select";
                Window.InformationList.ScrollIntoView(Window.InformationList.Items[Window.InformationList.Items.Count - 1]);
            }
        }
        private static void CopyOrMoveFiles(string outputDirectory, FileInfo fileInfo, bool copy)
        {
            if (copy == true) File.Copy($"{fileInfo.File}", $"{(outputDirectory == "" ? fileInfo.Dire : outputDirectory)}\\{fileInfo.Name}{fileInfo.Exte}", true);
            else File.Move($"{fileInfo.File}", $"{(outputDirectory == "" ? fileInfo.Dire : outputDirectory)}\\{fileInfo.Name}{fileInfo.Exte}");
        }
        private void SelectFiles(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Multiselect = true };
            if (openFileDialog.ShowDialog() != true) return;
            FilePaths = openFileDialog.FileNames;
            Window.SelectFilesButton.Content = $"Select ({FilePaths.Length})";
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
    }
}
