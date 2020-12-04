using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Threading.Tasks;
using RenamerApp.WPFClasses;
using System.Windows.Controls;
using System.Diagnostics;

namespace RenamerApp
{
    class Operations
    {
        private ILogger Logger { get; }
        private readonly EditorWindow _window;
        private WindowInputs WindowInputs { get; set; }
        private string[] FilePaths { get; set; }
        public Operations(EditorWindow window, ILogger logger)
        {
            Logger = logger;
            _window = window;
            _window.StartButton.Click += StartOperation;
            _window.SelectFilesButton.Click += SelectFiles;
            _window.SelectOutputButton.Click += SelectOutputFolder;
            _window.ContextItem1.Click += ContextItem1_Click;
            _window.ResetUiButton.Click += ResetUi;
            _window.HelpButton.Click += ShowHelpText;
        }
        private async void StartOperation(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowInputs = new WindowInputs(_window);
                WindowInputs.SetStartButtonContent("Stop !!!");
                WindowInputs.SetProgressBarPercentage(false);
                WindowInputs.SetProgressBarValue(0);
                Logger.Clear();
                if (FilePaths == null)
                {
                    Logger.Log("No files selected");
                    return;
                }
                WindowInputs.SetProgressBarPercentage(true);
                Logger.Log("Starting operation - Please wait");

                _window.StartButton.Click += EmergencyStopOperation;
                WindowInputs.SetProgressBarMaxmimum(FilePaths.Length);
                foreach (string file in FilePaths)
                {
                    var fileInfo = new FileInputs(file) { Copy = WindowInputs.CopyCheckBox, OutputDirectory = WindowInputs.OutputDirectory };
                    var fileNameEditor = new FileNameEditor(fileInfo);
                    var errorChecking = new ErrorChecking(fileInfo, WindowInputs, Logger);
                    //Under kan endres hva som skjer med navnet
                    if (WindowInputs.SpecificStringThis != "") fileNameEditor.ReplaceSpecificString(WindowInputs.SpecificStringThis, WindowInputs.SpecificStringWith);
                    if (WindowInputs.FromIndex != "") fileNameEditor.SubstringThis(WindowInputs.FromIndex, WindowInputs.ToIndex);
                    if (WindowInputs.TrimCheckBox == true) fileNameEditor.Trim();
                    fileNameEditor.UpperCase(WindowInputs.UppercaseCheckBox);
                    Logger.Log(fileInfo.LogStartProcessing);
                    //Forskjellig error checking
                    if (errorChecking.DirectoryExistsOrNot() == false) continue;
                    if (errorChecking.FileExistsAndCopyEnabledAndDirectoryDefault() == false) continue;
                    if (errorChecking.FileExistsAndOverwriteNotChecked() == false) continue;
                    errorChecking.FileExistsAndOverwriteChecked();
                    //Output ting her nede
                    await CopyOrMoveFilesAsync(fileInfo.OutputDirectory, fileInfo, WindowInputs.CopyCheckBox, (bool)WindowInputs.OverwriteCheckBox);
                    WindowInputs.IncrementProgressBar();
                    Logger.Log(fileInfo.LogFinishedProcessing);
                }
                Logger.Log("Operation finished");
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            finally
            {
                _window.StartButton.Click -= EmergencyStopOperation;
                WindowInputs.SetStartButtonContent("Start");
                FilePaths = null;
                WindowInputs.SetSelectedFilesText("");
                _window.InformationList.ScrollIntoView(_window.InformationList.Items[^1]);
            }
        }
        private async Task<bool> CopyOrMoveFilesAsync(string outputDirectory, FileInputs fileInputs, bool? copy, bool overwrite)
        {
            if (copy == true) await Task.Run(() => File.Copy($"{fileInputs.FullFile}", $"{(outputDirectory == "" ? fileInputs.Dire : outputDirectory)}\\{fileInputs.Name}{fileInputs.Exte}", overwrite));
            else await Task.Run(() => File.Move($"{fileInputs.FullFile}", $"{(outputDirectory == "" ? fileInputs.Dire : outputDirectory)}\\{fileInputs.Name}{fileInputs.Exte}", overwrite));
            return true;
        }
        private void SelectFiles(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Multiselect = true };
            if (openFileDialog.ShowDialog() != true) return;
            FilePaths = openFileDialog.FileNames;
            _window.SelectedFilesText.Text = $"Selected {FilePaths.Length} {(FilePaths.Length < 2 ? "File" : "Files")}";
        }
        private void SelectOutputFolder(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog { IsFolderPicker = true };
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result != CommonFileDialogResult.Ok) return;
            _window.OutputDirectoryInputBox.Text = dialog.FileName;
        }
        private void ContextItem1_Click(object sender, RoutedEventArgs e)
        {
            if (_window.InformationList.SelectedItem == null) return;
            Clipboard.SetText(_window.InformationList.SelectedItem.ToString() ?? throw new InvalidOperationException());
        }
        private void ShowHelpText(object sender, RoutedEventArgs e)
        {
            var helpText = new HelpTextList();
            var helpList = new ListBox();
            foreach (var text in helpText.TextList)
            {
                helpList.Items.Add(text);
            }
            var helpDialog = new EditorModalWindow(693, 383) { Title = "Help", Owner = _window, Content = helpList, MaxWidth = 693, MaxHeight = 383 };
            helpDialog.Show();
        }
        private void ResetUi(object sender, RoutedEventArgs e)
        {
            WindowInputs = new WindowInputs(_window);
            Logger.Clear();
            FilePaths = null;
            WindowInputs.ResetAllInputs();
        }
        private void EmergencyStopOperation(object sender, RoutedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
