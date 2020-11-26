using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace RenamerApp
{
    class EditorWindow : Window
    {
        private ListBox informationList;
        private TextBox outputDirectoryInputBox;
        private CheckBox upperCaseCheckBox;
        private CheckBox trimCheckBox;
        private string[] filePaths = null;

        public EditorWindow()
        {
            var grid = new Grid();
            informationList = new EditorInformationList();
            outputDirectoryInputBox = new EditorTextBox("Output Path", 200, 100, 70);
            upperCaseCheckBox = new EditorCheckBox(200, 20, 350, 40, "Uppercase");
            trimCheckBox = new EditorCheckBox(200, 20, 350, 70, "Trim");
            var startButton = new EditorButton(0, 0, "Start");
            var stopButton = new EditorButton(0, 50, "Select");
            startButton.Click += StartOperation;
            stopButton.Click += SelectFiles;

            grid.Children.Add(informationList);
            grid.Children.Add(upperCaseCheckBox);
            grid.Children.Add(trimCheckBox);
            grid.Children.Add(outputDirectoryInputBox);
            grid.Children.Add(startButton);
            grid.Children.Add(stopButton);

            Content = grid;
        }
        private void StartOperation(object sender, RoutedEventArgs e)
        {
            informationList.Items.Clear();
            string outputDirectory = outputDirectoryInputBox.Text;
            if (filePaths == null)
            {
                informationList.Items.Add("No files found");
                return;
            }
            foreach (string file in filePaths)
            {
                try
                {
                    string dire = Path.GetDirectoryName(file);
                    string name = Path.GetFileNameWithoutExtension(file);
                    string exte = Path.GetExtension(file);
                    string oldn = Path.GetFileNameWithoutExtension(file);
                    //Under kan endres hva som skjer med navnet
                    //name = name.Substring(6);
                    //name = name.Replace("_", " ");
                    //name = name.Replace("  ", " ");
                    if (trimCheckBox.IsChecked == true) name = name.Trim();
                    name = upperCaseCheckBox.IsChecked == true ? name.Substring(0, 1).ToUpper() + name[1..] : name.Substring(0, 1).ToLower() + name[1..];
                    informationList.Items.Add($"Renamed \"{oldn}\" to: \"{name}{exte}\" {(outputDirectory == "" ? "" : $"and moved file to {outputDirectory}")}");
                    //Her bestemmer man hvor det skal outputtes til
                    File.Move($"{file}", $"{(outputDirectory == "" ? dire : outputDirectory)}\\{name}{exte}");
                }
                catch (Exception)
                {
                    informationList.Items.Add("Error renaming files");
                }
                finally { filePaths = null; }
            }
        }
        private void SelectFiles(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == true)
            {
                filePaths = openFileDialog.FileNames;
            }
        }
    }
}
