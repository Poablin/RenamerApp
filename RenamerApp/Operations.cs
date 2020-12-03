using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Threading.Tasks;
using RenamerApp.WPFClasses;
using System.Windows.Controls;

namespace RenamerApp
{
    class Operations
    {
        private ILogger Logger { get; }
        private EditorWindow Window { get; }
        private WindowInputs WindowInputs { get; set; }
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
            Window.HelpButton.Click += ShowHelpText;
        }
        private async void StartOperation(object sender, RoutedEventArgs e)
        {
            WindowInputs = new WindowInputs(Window);
            WindowInputs.SetProgressBarValue(0);
            Logger.Clear();
            if (FilePaths == null)
            {
                Logger.Log("No files selected");
                return;
            }
            Logger.Log("Starting operation - Please wait");
            try
            {
                WindowInputs.SetProgressBarMaxmimum(FilePaths.Length);
                WindowInputs.SetProgressBarPercentage();
                foreach (string file in FilePaths)
                {
                    var fileInfo = new FileInfo(file) { Copy = WindowInputs.CopyCheckBox, OutputDirectory = WindowInputs.OutputDirectory };
                    var fileNameEditor = new FileNameEditor(fileInfo);
                    var errorChecking = new ErrorChecking(fileInfo, WindowInputs, Logger);

                    //Under kan endres hva som skjer med navnet
                    if (WindowInputs.SpecificStringThis != "") fileNameEditor.ReplaceSpecificString(WindowInputs.SpecificStringThis, WindowInputs.SpecificStringWith);
                    if (WindowInputs.FromIndex != "") fileNameEditor.DeleteEverythingElse(WindowInputs.FromIndex, WindowInputs.ToIndex);
                    if (WindowInputs.TrimCheckBox == true) fileNameEditor.Trim();
                    fileNameEditor.UpperCase(WindowInputs.UppercaseCheckBox);
                    Logger.Log(fileInfo.LogStartProcessing);
                    //Forskjellig error checking
                    if (errorChecking.DirectoryExistsOrNot() == false) continue;
                    if (errorChecking.FileExistsAndCopyEnabledAndDirectoryDefault() == false) continue;
                    if (errorChecking.FileExistsAndOverwriteNotChecked() == false) continue;
                    errorChecking.FileExistsAndOverwriteChecked();
                    //Output ting her nede
                    await Task.Run(() => CopyOrMoveFiles(fileInfo.OutputDirectory, fileInfo, WindowInputs.CopyCheckBox, (bool)WindowInputs.OverwriteCheckBox));
                    WindowInputs.IncrementProgressBar();
                    Logger.Log(fileInfo.LogFinishedProcessing);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            finally
            {
                Logger.Log("Operation finished");
                FilePaths = null;
                WindowInputs.SetSelectedFilesText("");
                Window.InformationList.ScrollIntoView(Window.InformationList.Items[^1]);
            }
        }
        private static void CopyOrMoveFiles(string outputDirectory, FileInfo fileInfo, bool? copy, bool overwrite)
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
        private void ShowHelpText(object sender, RoutedEventArgs e)
        {
            //var context = new Editor
            var helpText = new HelpTextList();
            var helpList = new ListBox();
            foreach (var text in helpText.TextList)
            {
                helpList.Items.Add(text);
            }
            var helpDialog = new EditorModalWindow() { Title = "Help", Owner = Window, Content = helpList };
            helpDialog.Show();
        }
        private void ResetUi(object sender, RoutedEventArgs e)
        {
            WindowInputs = new WindowInputs(Window);
            Logger.Clear();
            FilePaths = null;
            WindowInputs.ResetAllInputs();
        }
    }
}
