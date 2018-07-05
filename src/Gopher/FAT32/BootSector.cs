using System;
using System.Collections.Generic;
using System.Text;

namespace Gopher
{
    public class BootSector
    {
        public string OEMname="";
        public UInt32 BPS;
        public UInt32 SPC;
        public UInt32 SPT;
        public UInt32 reserved;
        public UInt32 FATcount;
        public UInt32 Totalsector;
        public UInt32 media;
        public UInt32 hiddenSector;
        public UInt32 FATsize;
        public UInt32 FSVersion;
        public UInt32 RootClus;
        public UInt32 FSInfo;
        public UInt32 driveNum;
        public UInt32 volumeID;
        public string volLabel="";
    }
}
