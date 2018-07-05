using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Win32.SafeHandles;

namespace Gopher
{
    class SectorReader
    {
        public static Partition[] readMBR(int hardNum)
        {
            SafeFileHandle handle = WindowsAPI.Open(@"\\.\PhysicalDrive" + hardNum);

            byte[] buffer = new byte[512];
            Partition[] partition = new Partition[4];
            buffer = WindowsAPI.ReadFile(handle,buffer, 0,512);

            if (!handle.IsInvalid)
            {
                if (buffer[0x1FE] == 0x55 && buffer[0x1FF] == 0xAA)
                {
                    //check the four partition entries:
                    for (int i = 0; i < 4; i++)
                    {
                        int beginOffset = 0x1BE + i * 0x10;

                        if (buffer[beginOffset] == 0x00 || buffer[beginOffset] == 0x80)
                        {


                            partition[i] = new Partition(i.ToString());
                            if (buffer[beginOffset] == 0x80) partition[i].bootable = true;
                            partition[i].headBegin = buffer[beginOffset + 1];

                            partition[i].sectorBegin =(UInt32) (buffer[beginOffset + 2] & 0x3F);
                            partition[i].cylinderBegin = (UInt32)(buffer[beginOffset + 2] & 0xC0) * 4 + buffer[beginOffset + 3];

                            partition[i].partitionType = (UInt32)buffer[beginOffset + 4];
                            partition[i].headEnd =(UInt32)  buffer[beginOffset + 5];
                            partition[i].sectorEnd = (UInt32) (buffer[beginOffset + 6] & 0x3F);
                            partition[i].cylinderEnd = (UInt32)(buffer[beginOffset + 6] & 0xC0) * 4 + buffer[beginOffset + 7];

                            partition[i].sectorNum = (UInt32) (buffer[beginOffset + 0x0C] + buffer[beginOffset + 0x0D] * 0x100 + buffer[beginOffset + 0x0E] * 0x10000 + buffer[beginOffset + 0x0F] * 0x1000000);



                        }
                        else System.Console.WriteLine("Partition " + i + " was inactive.");
                    }
                }
            }

            return partition;
        }

        public static unsafe BootSector BSreader(string volName)
        {
            SafeFileHandle handle = WindowsAPI.CreateFile
                (
                @"\\.\"+volName,
                WindowsAPI.GENERIC_READ,
                WindowsAPI.FILE_SHARE_WRITE|WindowsAPI.FILE_SHARE_READ,
                0,
                WindowsAPI.OPEN_EXISTING,
                0,
                0);

            UInt32 error = WindowsAPI.GetLastError();

            byte[] buffer = new byte[512];
            int n = 0;
            byte read = 0;

            BootSector myBS = new BootSector();

            if (!handle.IsInvalid)
            {
                WindowsAPI.OverLapped over = new WindowsAPI.OverLapped();
                over.handle = IntPtr.Zero;
                over.offset.offset = 0;
                over.offset.offsetHigh = 0;

                fixed (byte* p = buffer)
                {
                    read = WindowsAPI.ReadFile(handle,p, 512, &n, 0);            
                    error = WindowsAPI.GetLastError();

                    for(int i=3;i<11;i++)
                     myBS.OEMname += (char)buffer[i];

                    uint pow=1;
                    uint sum = 0;
                    for (int i = 11; i <=12; i++)
                    {
                        sum+=pow*buffer[i];
                        pow*=256;
                    }

                    myBS.BPS = sum;

                    pow=1;
                    sum = 0;
                    for (int i = 13; i <=13; i++)
                    {
                        sum+=pow*buffer[i];
                        pow *= 256;
                    }

                    myBS.SPC = sum;

                    pow=1;
                    sum = 0;
                    for (int i = 14; i <=15; i++)
                    {
                        sum+=pow*buffer[i];
                        pow *= 256;
                    }

                    myBS.reserved = sum;

                    pow=1;
                    sum = 0;
                    for (int i = 16; i <= 16; i++)
                    {
                        sum+=pow*buffer[i];
                        pow *= 256;
                    }

                    myBS.FATcount = sum;

                    pow=1;
                    sum = 0;
                    for (int i = 21; i <= 21; i++)
                    {
                        sum+=pow*buffer[i];
                        pow *= 256;
                    }

                    myBS.media = sum;

                    pow = 1;
                    sum = 0;
                    for (int i = 24; i <= 25; i++)
                    {
                        sum += pow * buffer[i];
                        pow *= 256;
                    }

                    myBS.SPT = sum;

                    pow=1;
                    sum = 0;
                    for (int i = 32; i <=35; i++)
                    {
                        sum+=pow*buffer[i];
                        pow *= 256;
                    }

                    myBS.Totalsector=sum;

                    pow=1;
                    sum = 0;
                    for (int i = 28; i <=31; i++)
                    {
                        sum+=pow*buffer[i];
                        pow *= 256;
                    }

                    myBS.hiddenSector = sum;

                    pow=1;
                    sum = 0;
                    for (int i = 36; i <=39; i++)
                    {
                        sum+=pow*buffer[i];
                        pow *= 256;
                    }

                    myBS.FATsize = sum;

                    pow=1;
                    sum = 0;
                    for (int i = 44; i<47; i++)
                    {
                        sum+=pow*buffer[i];
                        pow *= 256;
                    }

                    myBS.RootClus = sum;

                    pow=1;
                    sum = 0;
                    for (int i = 64; i <=64; i++)
                    {
                        sum+=pow*buffer[i];
                        pow *= 256;
                    }

                    myBS.driveNum = sum;

                    pow=1;
                    sum = 0;
                    for (int i = 67; i <=70; i++)
                    {
                        sum+=pow*buffer[i];
                        pow *= 256;
                    }

                    myBS.volumeID = sum;

                    pow=1;
                    sum = 0;
                    for (int i = 71; i <82; i++)
                    {
                        myBS.volLabel+=(char)buffer[i];
                    }

                }
            }

            return myBS;
        }
    }
}
