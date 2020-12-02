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
        private WindowInputs WindowInputs { get; set; }

        public Operations(EditorWindow window, ILogger logger)
        {
            Logger = logger;
            Window = window;
            WindowInputs = new WindowInputs(window);
            Window.StartButton.Click += StartOperation;
            Window.SelectFilesButton.Click += SelectFiles;
            Window.SelectOutputButton.Click += SelectOutputFolder;
            Window.ContextItem1.Click += ContextItem1_Click;
            Window.ResetUiButton.Click += ResetUi;
        }
        private async void StartOperation(object sender, RoutedEventArgs e)
        {
            var outputDirectory = Window.OutputDirectoryInputBox.Text;
            Window.ProgressBar.Value = 0;
            Logger.Clear();
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
                    var fileInfo = new FileInfo(file) { Copy = WindowInputs.CopyCheckBox, OutputDirectory = outputDirectory };
                    var fileNameEditor = new FileNameEditor(fileInfo);
                    //Under kan endres hva som skjer med navnet
                    if (WindowInputs.FromIndex != "") fileNameEditor.DeleteEverythingElse(WindowInputs.FromIndex, WindowInputs.ToIndex);
                    if (WindowInputs.SpecificStringThis != "") fileNameEditor.ReplaceSpecificString(WindowInputs.SpecificStringThis, WindowInputs.SpecificStringWith);
                    if (WindowInputs.TrimCheckBox == true) fileNameEditor.Trim();
                    fileNameEditor.UpperCase(WindowInputs.UppercaseCheckBox);
                    //Her bestemmer man hvor det skal outputtes til
                    Logger.Log(fileInfo.LogStartProcessing);
                    await Task.Run(() => CopyOrMoveFiles(outputDirectory, fileInfo, WindowInputs.CopyCheckBox));
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
        private static void CopyOrMoveFiles(string outputDirectory, FileInfo fileInfo, bool? copy)
        {
            if (copy == true) File.Copy($"{fileInfo.File}", $"{(outputDirectory == "" ? fileInfo.Dire : outputDirectory)}\\{fileInfo.Name}{fileInfo.Exte}", true);
            else File.Move($"{fileInfo.File}", $"{(outputDirectory == "" ? fileInfo.Dire : outputDirectory)}\\{fileInfo.Name}{fileInfo.Exte}");
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
            Window.TrimCheckBox.IsChecked = false;
            Window.CopyCheckBox.IsChecked = false;
        }
    }
}
