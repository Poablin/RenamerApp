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

        public EditorWindow()
        {
            var grid = new Grid();
            informationList = new EditorInformationList();
            directoryInputBox = new EditorTextBox("Directory Path", 200, 100, 50);
            outputDirectoryInputBox = new EditorTextBox("Output Path", 200, 100, 70);
            upperCaseCheckBox = new EditorCheckBox(200, 20, 350, 70, "Uppercase");
            var startButton = new EditorButton(0, 0, "Start");
            var stopButton = new EditorButton(0, 50, "Stop");
            startButton.Click += StartOperation;

            grid.Children.Add(informationList);
            grid.Children.Add(upperCaseCheckBox);
            grid.Children.Add(directoryInputBox);
            grid.Children.Add(outputDirectoryInputBox);
            grid.Children.Add(startButton);
            grid.Children.Add(stopButton);

            Content = grid;
        }
        private void StartOperation(object sender, RoutedEventArgs e)
        {
            string directory = directoryInputBox.Text;
            string outputDirectory = outputDirectoryInputBox.Text;

            string[] filePaths = Directory.GetFiles(directory);
           
            informationList.Items.Clear();

            foreach (string file in filePaths)
            {
                try
                {
                    string dire = Path.GetDirectoryName(file);
                    string name = Path.GetFileNameWithoutExtension(file);
                    string exte = Path.GetExtension(file);
                    //Under kan endres hva som skjer med navnet
                    //name = name.Substring(6);
                    //name = name.Replace("_", " ");
                    //name = name.Replace("  ", " ");
                    if (upperCaseCheckBox.IsChecked == true)
                    {
                        name = name.Substring(0, 1).ToUpper() + name.Substring(1);
                    }
                    //Her bestemmer man hvor det skal outputtes til
                    informationList.Items.Add("Copying file for: " + name);
                    File.Copy($"{file}", $"{outputDirectory}\\{name}{exte}");
                }
                catch (Exception)
                {
                    informationList.Items.Add("Error copying file");
                }
            }
        }
    }
}
