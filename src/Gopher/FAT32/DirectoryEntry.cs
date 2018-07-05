using System;
using System.Collections.Generic;
using System.Text;
using Gopher;

namespace Gopher
{
    public class DirectoryEntry
    {
        public enum attributes
        {
            Directory, Read_Only, Hidden, System, Vollume_ID, Archive, Long_Name
        };
        public enum EntryType
        {
            Deleted, Valid_long, Valid_short, End_of_Dir
        };
        

        #region Fields

        public string longName = "";
        public string shortName = "";
        public attributes attribute;

        private int crtTimeSecond;
        private int crtTimeMinute;
        private int crtTimeHour;

        private int crtDateDay;
        private int crtDateMonth;
        private int crtDateYear;

        private int lastAccDateDay;
        private int lastAccDateMonth;
        private int lastAccDateYear;

        private int writeDateDay;
        private int writeDateMonth;
        private int writeDateYear;

        private int writeTimeSecond;
        private int writeTimeMinute;
        private int writeTimeHour;

        public UInt32 firstCluster;
        public UInt32 FileSize;
        public Partition partition;
        public String fullPath;
        public bool isPartition = false;
        #endregion

        #region Properties

        public string CreationDate
        {
            get
            {
                return crtDateDay + "\\" +crtDateMonth+ "\\" + crtDateYear;
            }
        }

        public string CreationTime
        {
            get
            {
                return crtTimeHour + ":" + crtTimeMinute + ":" + crtTimeSecond*2;
            }
        }

        public string LastAccessDate
        {
            get
            {
                return lastAccDateDay + "\\" + lastAccDateMonth + "\\" + lastAccDateYear;
            }
        }

        public string writeDate
        {
            get
            {
                return writeDateDay + "\\" + writeDateMonth + "\\" + writeDateYear;
            }
        }

        public string writeTime
        {
            get
            {
                return writeTimeHour + ":" + writeTimeMinute + ":" + writeTimeSecond * 2;
            }
        }
        
        public string Name
        {
            get
            {
                if (longName == "") return shortName;
                return longName;
            }
        }
#endregion

        public DirectoryEntry(byte[] entry,Partition partition)
        {
            this.partition = partition;
            translate(entry);
        }

        public static EntryType type(byte[] entry)
        {
            if (entry[0] == 0xE5) return EntryType.Deleted;
            else if (entry[0] == 0) return EntryType.End_of_Dir;
            else if ((entry[11]&0x3f) == 0xf) return EntryType.Valid_long;
            return EntryType.Valid_short;
        }

        private void translate(byte[] entry)
        {
            UInt32 pow = 1;

            int i=0;
            for(i=0;i<=10;i++) shortName+=(char)entry[i];

            if (entry[i] == 0x10) attribute = attributes.Directory;
            else if (entry[i] == 0x02) attribute = attributes.Hidden;
            else if (entry[i] == 0x04) attribute = attributes.System;
            else if (entry[i] == 0x08) attribute = attributes.Vollume_ID;
            else if (entry[i] == 0x01) attribute = attributes.Read_Only;
            else if (entry[i] == 0x20) attribute = attributes.Archive;
            else if (entry[i] == 0x40) attribute = attributes.Long_Name;
            else attribute = attributes.Archive;

            if (attribute != attributes.Directory)
                shortName = shortName.Substring(0, 8).Trim() + "." + shortName.Substring(8, 3).Trim();
            else 
                shortName = shortName.Trim();

            pow = 1;
            for (i = 26; i <= 27; i++)
            {
                firstCluster+=pow*entry[i];
                pow *= 256;
            }
            for (i = 20; i <= 21; i++)
            {
                firstCluster += pow * entry[i];
                pow *= 256;
            }

            pow = 1;
            for (i = 28; i <= 31; i++)
            {
                FileSize += entry[i] * pow;
                pow *= 256;
            }

            //Calculating Creation Time 
            crtTimeSecond = entry[14] % 32;
            crtTimeMinute = entry[14]/32+(entry[15] % 8)*8;
            crtTimeHour = entry[15] / 8;
            if (crtTimeHour == 0) crtTimeHour = 12;

            //calculating Creation Date
            crtDateDay = entry[16] % 32;
            crtDateMonth = entry[16] / 32 + (entry[17] % 2)*2;
            crtDateYear =1980+ entry[17] / 2;

            //calculating Last Access Date
            lastAccDateDay = entry[18] % 32;
            lastAccDateMonth = entry[18] / 32 + (entry[19] % 2)*2;
            lastAccDateYear = 1980 + entry[19] / 2;

            //Calculating Write Time 
            writeTimeSecond = entry[22] % 32;
            writeTimeMinute = entry[22] / 32 + (entry[23] % 8)*8;
            writeTimeHour = entry[23] / 8;
            if (writeTimeHour == 0) writeTimeHour = 12;

            //calculating Creation Date
            writeDateDay = entry[24] % 32;
            writeDateMonth = entry[24] / 32 + (entry[25] % 2)*2;
            writeDateYear = 1980 + entry[25] / 2;
        }

        public bool hasSubDir()
        {
            return partition.hasSubDir(this.firstCluster);
        }

        public DirectoryEntry[] getDirectories()
        {
            return partition.getDirectoryData(this.firstCluster);
        }
    }
}