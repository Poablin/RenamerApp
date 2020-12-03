using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenamerApp
{
    class HelpTextList
    {
        public string[] TextList = new string[17];
        public HelpTextList()
        {
            TextList[0] = "STOP !!! BUTTON - Emergency stops all threads and exists program. Files being overwritten will have backups restored.";
            TextList[1] = string.Empty;
            TextList[2] = "SELECT BUTTON - Select the files you want to rename, move or copy.";
            TextList[3] = "OUTPUT BUTTON - Select the folder where you want files to be output to.";
            TextList[4] = "OUTPUT TEKSTBOX - Write the path of the folder you want to output to. Leave blank for same folder as the files you selected.";
            TextList[5] = "Example: \"C:\\Test\\Output\"";
            TextList[6] = string.Empty;
            TextList[7] = "REPLACE INPUT - Write the exact text you want to replace in the file, and what you want to replace it with.";
            TextList[8] = "Example: \"TEST.txt\" replace TEST with Test makes \"Test.txt\"";
            TextList[9] = "KEEP INPUT - Number of the exact index range you want to keep. Leaving box empty uses length of name. Index starts at 0.";
            TextList[10] = "Example: \"Testing.txt\" index 0 to 3 makes \"Tes.txt\"";
            TextList[11] = "Example: \"Testing.txt\" index 3 to empty makes \"ting.txt\"";
            TextList[12] = string.Empty;
            TextList[13] = "COPY FILES - The files you select will be copied instead of moved.";
            TextList[14] = "OVERWRITE - Selected files will be overwritten if they already exist in the output directory.";
            TextList[15] = "UPPERCASE - File names wil always have an uppercase first letter if checked, otherwise it'll always be lower case.";
            TextList[16] = "TRIM - Spaces on the ends of file names will be removed.";
        }
    }
}
