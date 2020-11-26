using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace RenamerApp
{
    class EditorWindow : Window
    {
        private ListBox InformationList { get; }
        private TextBox OutputDirectoryInputBox { get; }
        private CheckBox CopyCheckBox { get; }
        private CheckBox UpperCaseCheckBox { get; }
        private CheckBox TrimCheckBox { get; }
        private EditorButton SelectButton { get; }
        private string[] FilePaths { get; set; }

        public EditorWindow()
        {
            var grid = new Grid();
            InformationList = new EditorInformationList();
            OutputDirectoryInputBox = new EditorTextBox("Output Path", 200, 100, 70);
            CopyCheckBox = new EditorCheckBox(200, 20, 350, 10, "Copy");
            UpperCaseCheckBox = new EditorCheckBox(200, 20, 350, 40, "Uppercase");
            TrimCheckBox = new EditorCheckBox(200, 20, 350, 70, "Trim");
            var startButton = new EditorButton(0, 0, "Start");
            SelectButton = new EditorButton(0, 50, "Select");
            startButton.Click += StartOperation;
            SelectButton.Click += SelectFiles;

            grid.Children.Add(InformationList);
            grid.Children.Add(CopyCheckBox);
            grid.Children.Add(UpperCaseCheckBox);
            grid.Children.Add(TrimCheckBox);
            grid.Children.Add(OutputDirectoryInputBox);
            grid.Children.Add(startButton);
            grid.Children.Add(SelectButton);

            Content = grid;
        }
        private void StartOperation(object sender, RoutedEventArgs e)
        {
            InformationList.Items.Clear();
            string outputDirectory = OutputDirectoryInputBox.Text;
            if (FilePaths == null)
            {
                InformationList.Items.Add("No files found");
                return;
            }
            try
            {
                foreach (string file in FilePaths)
                {
                    string dire = Path.GetDirectoryName(file);
                    string name = Path.GetFileNameWithoutExtension(file);
                    string exte = Path.GetExtension(file);
                    string oldn = Path.GetFileNameWithoutExtension(file);
                    //Under kan endres hva som skjer med navnet
                    //name = name.Substring(6);
                    //name = name.Replace("_", " ");
                    //name = name.Replace("  ", " ");
                    if (TrimCheckBox.IsChecked == true) name = name.Trim();
                    name = UpperCaseCheckBox.IsChecked == true ? name.Substring(0, 1).ToUpper() + name[1..] : name.Substring(0, 1).ToLower() + name[1..];
                    //Her bestemmer man hvor det skal outputtes til
                    if (CopyCheckBox.IsChecked == true)
                    {
                        File.Copy($"{file}", $"{(outputDirectory == "" ? dire : outputDirectory)}\\{name}{exte}");
                        InformationList.Items.Add($"Renamed \"{oldn}\" to \"{name}{exte}\" {(outputDirectory == "" ? "" : $"and copied file to {outputDirectory}")}");
                    }
                    else 
                    { 
                        File.Move($"{file}", $"{(outputDirectory == "" ? dire : outputDirectory)}\\{name}{exte}");
                        InformationList.Items.Add($"Renamed \"{oldn}\" to \"{name}{exte}\" {(outputDirectory == "" ? "" : $"and moved file to {outputDirectory}")}");
                    }
                }
            }
            catch (Exception ex)
            {
                InformationList.Items.Add(ex);
            }
            finally { FilePaths = null; SelectButton.Content = "Select"; }

        }
        private void SelectFiles(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                FilePaths = openFileDialog.FileNames;
                SelectButton.Content = $"Select ({FilePaths.Length})";
            }
        }
    }
}
