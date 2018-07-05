using System;
using System.Collections.Generic;
using System.Text;

namespace Gopher
{
    public class LongDirectoryEntry
    {
        public string longName;
        public Int32 currentOrder;
        public byte checkSum;
        public bool isValidLongEntry;
      

        public LongDirectoryEntry(byte[] longEntry)
        {
            this.checkSum = longEntry[13];
            this.longName = extractName(longEntry);

        }

        public bool merge(byte[] longEntry)
        {
            if (checkSum != longEntry[13]) return false;

            string newSubName = extractName(longEntry);
            longName = newSubName + longName;
            return true;
        }

        private string extractName(byte[] entry)
        {
            char[] longSubName = new Char[13];
            int subIndex = 0;
            Int32 newChar;
            short i = 0;

            for (i = 1; i < 11; i += 2, subIndex++)
            {
                newChar = Convert.ToInt32(entry[i] + 256 * entry[(i + 1)]);
                if (newChar == 0xFFFF)
                    i = 11;
                else
                    longSubName[subIndex] = Convert.ToChar(newChar);
            }
            for (i = 14; i < 26; i += 2, subIndex++)
            {
                newChar = Convert.ToInt32(entry[i] + 256 * entry[(i + 1)]);
                if (newChar == 0xFFFF)
                    i = 26;
                else
                    longSubName[subIndex] = Convert.ToChar(newChar);
            }
            for (i = 28; i < 32; i += 2, subIndex++)
            {
                newChar = Convert.ToInt32(entry[i] + 256* entry[(i + 1)]);
                if (newChar == 0xFFFF)
                    i = 32;
                else
                    longSubName[subIndex] = Convert.ToChar(newChar);
            }
            string ret = "";
            for ( i = 0; i < 13; i++) if (longSubName[i] != 0) ret += longSubName[i];
            return ret;
        }

        public bool merge(DirectoryEntry entry)
        {
            //check checkSum       
            //return false
            //byte[] name=new byte[12];
            //name=entry.shortName.
            //byte sum = 0;
            //for (byte i = 11; i != 0; i--)
            //{
            //    sum=((sum & 1) ? 0x80:0)+(sum>>1)
            //}

            entry.longName = longName;
            return true;
        }
    }
}
