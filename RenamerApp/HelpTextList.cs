using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenamerApp
{
    class HelpTextList
    {
        public string[] TextList = new string[20];
        public HelpTextList()
        {
            TextList[0] = "Made by Poablin";
            TextList[1] = "";
            TextList[2] = "SELECT BUTTON: Select the files you want to rename, move or copy.";
            TextList[3] = "OUTPUT BUTTON: Select the folder where you want files to be output to.";
            TextList[4] = "OUTPUT TEKSTBOX: Write the path of the folder you want to output to, or leave blank for same folder as the files you selected.";
            TextList[5] = "Example: \"C:\\Test\\Output\"";
            TextList[6] = "";
            TextList[7] = "REPLACE INPUT: Write the exact text you want to replace something with in the file names.";
            TextList[8] = "Example: \"TEST.txt\" replace TEST with Test makes \"Test.txt\"";
            TextList[9] = "KEEP INPUT: Enter number of the exact index range you want to keep. Leaving To empty uses length of name. Index starts at 0.";
            TextList[10] = "Example: \"Testing.txt\" index 0 to 3 makes \"Tes.txt\"";
            TextList[11] = "Example: \"Testing.txt\" index 3 to empty makes \"ting.txt\"";
            TextList[12] = "";
            TextList[13] = "COPY FILES CHECK: The files will be copied instead of";
        }
    }
}
