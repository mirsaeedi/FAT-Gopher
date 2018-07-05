using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Gopher
{
    public class Partition
    {
        public BootSector bootSector;
        public string name;
        public string fileSystem = "";
        public bool bootable;
        public UInt32 headBegin;
        public UInt32 cylinderBegin;
        public UInt32 sectorBegin;
        public UInt32 partitionType;
        public UInt32 headEnd;
        public UInt32 cylinderEnd;
        public UInt32 sectorEnd;
        public UInt32 sectorNum;
        public UInt32 rootDirSectors = 0;
        public UInt32 firstDataSector;
        public PartitionType type;

        public enum PartitionType
        {
            Fixed, CD_ROM, Removable
        };

        public Partition(string name)
        {
            this.name = name;
            bootSector = SectorReader.BSreader(name); //BS maker
            firstDataSector = bootSector.FATsize * bootSector.FATcount + bootSector.reserved;
        }

        public UInt64 clusterByteAddress(UInt32 clusterNumber)
        {
            return sectorByteAddress(firstSectorOfCluster(clusterNumber));
        }

        public UInt64 sectorByteAddress(UInt32 sectorNumber)
        {
            return (sectorNumber * bootSector.BPS);
        }

        public UInt32 firstSectorOfCluster(UInt32 clusterNumber)
        {
            return (clusterNumber - 2) * bootSector.SPC + firstDataSector;
        }

        public int FatTypeDetermination()
        {
            return 0;
        }

        public UInt32 clusterFATSectorNum(UInt32 clusterNumber)
        {
            UInt32 FATOffset = clusterNumber * 4;
            return bootSector.reserved + FATOffset / bootSector.BPS;
        }

        public UInt32 clusterFatOffsetNum(UInt32 clusterNumber)
        {
            UInt32 FATOffset = clusterNumber * 4;
            return FATOffset % bootSector.BPS;
        }

        public bool hasSubDir(UInt32 clusterNum)
        {
            byte[] secBuff = new byte[bootSector.BPS];
            byte[] directoryEntryBuff = new byte[32];
            
            UInt64 byteAdd;

            
            SafeFileHandle handle = WindowsAPI.Open(@"\\.\" + this.name);
            

            do
            {
                byteAdd = clusterByteAddress(clusterNum);

                WindowsAPI.SetFilePointer(handle, byteAdd);
                int i = 64;
                for (int j = 0; j < bootSector.SPC; j++)
                {
                    secBuff = WindowsAPI.ReadFile(handle, secBuff, 0, secBuff.Length);
                   
                    for (; i < bootSector.BPS; i += 32)
                    {
                        if (secBuff[i] == 0x00)
                        {
                            handle.Close();
                            
                            return false;
                        }

                        for (int f = 0; f < 32; f++) directoryEntryBuff[f] = secBuff[f + i];
                        DirectoryEntry.EntryType type = DirectoryEntry.type(directoryEntryBuff);
                        if (type == DirectoryEntry.EntryType.Valid_short)
                        {
                            DirectoryEntry entry = new DirectoryEntry(directoryEntryBuff,null);
                            if (entry.attribute == DirectoryEntry.attributes.Directory)
                            {
                                return true;
                            }
                        }
                    } i = 0;
                }

                clusterNum = getNextChain(clusterNum);

            } while (clusterNum < 0x0FFFFFF8);

            return false;

        }

        public UInt32 getNextChain(UInt32 clusterNum)
        {
            SafeFileHandle fatHandle = WindowsAPI.Open(@"\\.\" + this.name);
            UInt32 a = clusterFATSectorNum(clusterNum);
            UInt32 b = clusterFatOffsetNum(clusterNum);
            UInt64 FATaddress = sectorByteAddress(a);
            WindowsAPI.SetFilePointer(fatHandle, FATaddress);
            Byte[] secBuff = new Byte[bootSector.BPS];
            secBuff = WindowsAPI.ReadFile(fatHandle, secBuff, 0, 512);


            UInt32 pow = 1;
            clusterNum = 0;
            for (UInt32 i = b; i < 4 + b; i++)
            {
                clusterNum += pow * secBuff[i];
                pow *= 256;
            }
            fatHandle.Close();
            return clusterNum;
        }

        public DirectoryEntry[] getDirectoryData(UInt32 clusterNum)
        {
            byte[] secBuff = new byte[bootSector.BPS];
            byte[] directoryEntryBuff = new byte[32];
            Queue<DirectoryEntry> directoryEntry = new Queue<DirectoryEntry>();
            UInt64 byteAdd;

            //TODO : to take care if address is 64 bit;
            SafeFileHandle handle = WindowsAPI.Open(@"\\.\" + this.name);
            SafeFileHandle fatHandle = WindowsAPI.Open(@"\\.\" + this.name );

            do
            {
                 byteAdd= clusterByteAddress(clusterNum);

                WindowsAPI.SetFilePointer(handle, byteAdd);
                bool inLongSet = false;
                LongDirectoryEntry longEntry = null;

                for (int j = 0; j < bootSector.SPC; j++)
                {
                    secBuff = WindowsAPI.ReadFile(handle, secBuff, 0, secBuff.Length);  
                
                    for (int i = 0; i < bootSector.BPS; i += 32)
                    {
                        if (secBuff[i] == 0x00)
                        {
                            handle.Close();
                            fatHandle.Close();
                            return directoryEntry.ToArray();
                        }

                        for (int f = 0; f < 32; f++) directoryEntryBuff[f] = secBuff[f + i];
                        DirectoryEntry.EntryType type=DirectoryEntry.type(directoryEntryBuff);
                        if ( type== DirectoryEntry.EntryType.Deleted)
                        {
                        
                        }
                        else if (type == DirectoryEntry.EntryType.Valid_long)
                        {
                           if(!inLongSet)
                           {
                               inLongSet=true ;
                               longEntry=new LongDirectoryEntry(directoryEntryBuff);
                           }
                           else
                               longEntry.merge(directoryEntryBuff);
                        }
                        else if(type==DirectoryEntry.EntryType.Valid_short)
                        {
                            DirectoryEntry entry=new DirectoryEntry(directoryEntryBuff,this);
                            if (entry.attribute != DirectoryEntry.attributes.Vollume_ID)
                            {
                                if (inLongSet) longEntry.merge(entry);
                                directoryEntry.Enqueue(entry);
                                inLongSet = false;
                            }
                        }
                    }
                }

                clusterNum = getNextChain(clusterNum);

            } while (clusterNum < 0x0FFFFFF8);

            return directoryEntry.ToArray();

            
        }
    }
}
