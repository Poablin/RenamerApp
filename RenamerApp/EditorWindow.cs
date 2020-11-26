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
        private TextBox directoryInputBox;
        private TextBox outputDirectoryInputBox;
        private CheckBox upperCaseCheckBox;
        private CheckBox trimCheckBox;

        public EditorWindow()
        {
            var grid = new Grid();
            informationList = new EditorInformationList();
            directoryInputBox = new EditorTextBox("Directory Path", 200, 100, 50);
            outputDirectoryInputBox = new EditorTextBox("Output Path", 200, 100, 70);
            upperCaseCheckBox = new EditorCheckBox(200, 20, 350, 40, "Uppercase");
            trimCheckBox = new EditorCheckBox(200, 20, 350, 70, "Trim");
            var startButton = new EditorButton(0, 0, "Start");
            var stopButton = new EditorButton(0, 50, "Select");
            startButton.Click += StartOperation;
            stopButton.Click += btnOpenFile_Click;

            grid.Children.Add(informationList);
            grid.Children.Add(upperCaseCheckBox);
            grid.Children.Add(trimCheckBox);
            grid.Children.Add(directoryInputBox);
            grid.Children.Add(outputDirectoryInputBox);
            grid.Children.Add(startButton);
            grid.Children.Add(stopButton);

            Content = grid;
        }
        private void StartOperation(object sender, RoutedEventArgs e)
        {
            try
            {
                informationList.Items.Clear();
                string directory = directoryInputBox.Text;
                string outputDirectory = outputDirectoryInputBox.Text;
                string[] filePaths = Directory.GetFiles(directory);
                foreach (string file in filePaths)
                {
                    try
                    {
                        string name = Path.GetFileNameWithoutExtension(file);
                        string exte = Path.GetExtension(file);
                        informationList.Items.Add("Renaming file: " + name + exte);
                        //Under kan endres hva som skjer med navnet
                        //name = name.Substring(6);
                        //name = name.Replace("_", " ");
                        //name = name.Replace("  ", " ");
                        if (trimCheckBox.IsChecked == true) name = name.Trim();
                        name = upperCaseCheckBox.IsChecked == true ? name.Substring(0, 1).ToUpper() + name[1..] : name.Substring(0, 1).ToLower() + name[1..];
                        informationList.Items.Add("Renamed file to: " + name + exte);
                        //Her bestemmer man hvor det skal outputtes til
                        File.Move($"{file}", $"{outputDirectory}\\{name}{exte}");
                    }
                    catch (Exception)
                    {
                        informationList.Items.Add("Error renaming files");
                    }
                }
            }
            catch (Exception ex) when (ex is ArgumentException || ex is DirectoryNotFoundException)
            {
                informationList.Items.Add("Path not found");
            }
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true) directoryInputBox.Text = Path.GetDirectoryName(openFileDialog.FileName);
        }
    }
}
