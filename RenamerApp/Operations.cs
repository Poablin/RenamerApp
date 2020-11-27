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
        private EditorWindow Window { get; }
        private string[] FilePaths { get; set; }
        public Operations(EditorWindow window)
        {
            Window = window;
            Window.StartButton.Click += StartOperation;
            Window.SelectFilesButton.Click += SelectFiles;
            Window.SelectOutputButton.Click += SelectOutputFolder;
            Window.ContextItem1.Click += ContextItem1_Click;
        }
        private async void StartOperation(object sender, RoutedEventArgs e)
        {
            Window.InformationList.Items.Clear();

            string outputDirectory = Window.OutputDirectoryInputBox.Text;
            if (FilePaths == null)
            {
                Window.InformationList.Items.Add("No files selected");
                return;
            }
            Window.InformationList.Items.Add("Started operation... please wait");
            try
            {
                foreach (string file in FilePaths)
                {
                    string dire = Path.GetDirectoryName(file);
                    string name = Path.GetFileNameWithoutExtension(file);
                    string exte = Path.GetExtension(file);
                    string oldn = Path.GetFileNameWithoutExtension(file);
                    Window.InformationList.Items.Add($"Started copying: {name}{exte}");
                    await Task.Run(() => CopyFiles(file, outputDirectory));
                    Window.InformationList.Items.Add($"{(oldn == name ? "" : $"Renamed \"{oldn}\" to \"{name}{exte}\" ")}{(outputDirectory == "" ? "" : $"Copied {name}{exte} to {outputDirectory}")}");
                }
            }
            catch (Exception ex)
            {
                Window.InformationList.Items.Add(ex);
            }
            finally { FilePaths = null; Window.SelectFilesButton.Content = "Select"; Window.InformationList.Items.Add("Operation finished!"); }

        }

        private void CopyFiles(string file, string outputDirectory)
        {
            string dire = Path.GetDirectoryName(file);
            string name = Path.GetFileNameWithoutExtension(file);
            string exte = Path.GetExtension(file);
            //Under kan endres hva som skjer med navnet
            //name = name.Substring(6);
            //name = name.Replace("_", " ");
            //name = name.Replace("  ", " ");
            //if (Window.TrimCheckBox.IsChecked == true) name = name.Trim();
            //name = Window.UpperCaseCheckBox.IsChecked == true ? name.Substring(0, 1).ToUpper() + name[1..] : name.Substring(0, 1).ToLower() + name[1..];
            //Her bestemmer man hvor det skal outputtes til


            File.Copy($"{file}", $"{(outputDirectory == "" ? dire : outputDirectory)}\\{name}{exte}");


            //else
            //{
            //    Window.InformationList.Items.Add($"Started moving: {name}{exte}");
            //    File.Move($"{file}", $"{(outputDirectory == "" ? dire : outputDirectory)}\\{name}{exte}");
            //    Window.InformationList.Items.Add($"{(oldn == name ? "" : $"Renamed \"{oldn}\" to \"{name}{exte}\" ")}{(outputDirectory == "" ? "" : $"Moved {name}{exte} to {outputDirectory}")}");
            //}
        }
        private void SelectFiles(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() != true) return;
            FilePaths = openFileDialog.FileNames;
            Window.SelectFilesButton.Content = $"Select ({FilePaths.Length})";
        }
        private void SelectOutputFolder(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
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
