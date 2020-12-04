using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenamerApp
{
    class TextArray
    {
        public static string[] HelpText()
        {
            var helpText = new string[17];
            helpText[0] = "Made by Poablin";
            helpText[1] = string.Empty;
            helpText[2] = "Select Button - Select the files you want to rename, move or copy.";
            helpText[3] = "Output Button - Select the folder where you want files to be output to.";
            helpText[4] = "Output Tekstbox - Manually write the output folder path. Leave blank for same folder as the files you selected.";
            helpText[5] = "* Example: \"C:\\Test\\Output\" *";
            helpText[6] = string.Empty;
            helpText[7] = "Replace Input - Write the exact text you want to replace in the file, and what you want to replace it with.";
            helpText[8] = "* Example: \"TEST.txt\" replace TEST with Test makes \"Test.txt\" *";
            helpText[9] = "Keep Input - Number of the exact index range you want to keep. Leaving box empty uses length of name. Index starts at 0.";
            helpText[10] = "* Example: \"Testing.txt\" index 0 to 3 makes \"Tes.txt\" *";
            helpText[11] = "* Example: \"Testing.txt\" index 3 to empty makes \"ting.txt\" *";
            helpText[12] = string.Empty;
            helpText[13] = "Copy files - The files you select will be copied instead of moved.";
            helpText[14] = "Overwrite - Selected files will overwrite if the names already exist in the output directory.";
            helpText[15] = "Uppercase - File names wil always have an uppercase first letter if checked, otherwise it'll always be lower case.";
            helpText[16] = "Trim - Spaces on the ends of file names will be removed.";
            return helpText;
        }
        public static string[] StopText()
        {
            var stopText = new string[20];
            stopText[0] = "                              Are you sure?                                   ";
            stopText[1] = "Application will shut down. Files being overwritten will have backups restored";
            return stopText;
        }
    }
}
