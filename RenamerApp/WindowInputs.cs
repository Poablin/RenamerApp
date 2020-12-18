using RenamerApp.WPFClasses;
using System;

namespace RenamerApp
{
    internal class WindowInputs
    {
        public WindowInputs(EditorWindow window)
        {
            Window = window;
            OutputDirectory = Window.OutputDirectoryInputBox.Text;
            StartButton = (string)Window.StartButton.Content;
            SelectedFiles = Window.SelectedFilesText.Text;
            SpecificStringThis = Window.SpecificStringReplaceThisInputBox.Text;
            SpecificStringWith = Window.SpecificStringReplaceWithInputBox.Text;
            FromIndex = Window.FromIndexInputBox.Text;
            ToIndex = Window.ToIndexInputBox.Text;
            TrimCheckBox = Window.TrimCheckBox.IsChecked;
            UppercaseCheckBox = Window.UpperCaseCheckBox.IsChecked;
            CopyCheckBox = Window.CopyCheckBox.IsChecked;
            OverwriteCheckBox = Window.OverwriteCheckBox.IsChecked;
            ProgressBarValue = Window.ProgressBar.Value;
            ProgressBarMaximum = Window.ProgressBar.Maximum;
        }

        private EditorWindow Window { get; }
        public string OutputDirectory { get; }
        public string StartButton { get; private set; }
        public string SelectedFiles { get; }
        public string SpecificStringThis { get; }
        public string SpecificStringWith { get; }
        public string FromIndex { get; }
        public string ToIndex { get; }
        public bool? TrimCheckBox { get; }
        public bool? UppercaseCheckBox { get; }
        public bool? CopyCheckBox { get; }
        public bool? OverwriteCheckBox { get; }
        public double ProgressBarValue { get; }
        public double ProgressBarMaximum { get; }

        public void SetProgressBarValue(int value)
        {
            Window.ProgressBar.Value = value;
        }

        public void IncrementProgressBar()
        {
            Window.ProgressBar.Value++;
            SetProgressBarPercentage(true);
        }

        public void SetProgressBarMaxmimum(int maxmimum)
        {
            Window.ProgressBar.Maximum = maxmimum;
        }

        public void SetProgressBarPercentage(bool started)
        {
            if (started == false)
            {
                Window.ProgressBarPercentageText.Text = "";
                return;
            }

            Window.ProgressBarPercentageText.Text =
                Convert.ToString(Math.Round(Window.ProgressBar.Value / Window.ProgressBar.Maximum * 100)) + "%";
        }

        public void SetSelectedFilesText(string text)
        {
            Window.SelectedFilesText.Text = text;
        }

        public void SetStartButtonContent(string text)
        {
            StartButton = text;
        }

        public void ResetAllInputs()
        {
            Window.OutputDirectoryInputBox.Text = "";
            Window.SelectedFilesText.Text = "";
            Window.SpecificStringReplaceThisInputBox.Text = "";
            Window.SpecificStringReplaceWithInputBox.Text = "";
            Window.FromIndexInputBox.Text = "";
            Window.ToIndexInputBox.Text = "";
            Window.TrimCheckBox.IsChecked = true;
            Window.UpperCaseCheckBox.IsChecked = true;
            Window.CopyCheckBox.IsChecked = false;
            Window.OverwriteCheckBox.IsChecked = false;
            Window.ProgressBar.Value = 0;
            Window.ProgressBar.Maximum = 1;
            Window.ProgressBarPercentageText.Text = "";
        }
    }
}