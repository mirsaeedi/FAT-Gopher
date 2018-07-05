using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;


namespace Gopher
{
    class WindowsAPI
    {
        #region Constants

        public static  uint FILE_SHARE_READ = 0x00000001;
        public static  uint FILE_SHARE_WRITE = 0x00000002;
        public static  uint FILE_SHARE_DELETE = 0x00000004;
        public static  uint FILE_SHARE_NON = 0;

        public static uint FILE_FLAG_OVERLAPPED = 0x40000000;

        public static  uint CREATE_ALWAYS = 2;
        public static  uint CREATE_NEW = 1;
        public static  uint OPEN_ALWAYS = 4;
        public static  uint OPEN_EXISTING = 3;
        
        public static  uint GENERIC_READ = 0x80000000;

        public static uint FILE_BEGIN = 0;
        public static uint FILE_CURRENT = 1;
        public static uint FILE_END = 2;

        #endregion

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern unsafe SafeFileHandle ExtractIcon
        (
            SafeHandle hInst,
            StringBuilder lpszExeFileName,
            UInt32 nIconIndex
        );
            
        [DllImport("kernel32.dll", SetLastError = true,CharSet=CharSet.Auto)]
        public static extern unsafe UInt32 GetLogicalDriveStrings
        (
            UInt32 nBufferLength,
            StringBuilder lpBuffer
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern UInt32 FindFirstVolume
        (
            StringBuilder  lpszVolumeName,
            UInt32 cchBufferLength
        );
        
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern unsafe UInt32 SetFilePointer
        (
            SafeFileHandle hFile,
            Int32 lDistanceToMove,
            Int32* lpDistanceToMoveHigh,
            UInt32 dwMoveMethod
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern SafeFileHandle CreateFile
        (
            string FileName,          // file name
            uint DesiredAccess,       // access mode
            uint ShareMode,           // share mode
            uint SecurityAttributes,  // Security Attributes
            uint CreationDisposition, // how to create
            uint FlagsAndAttributes,  // file attributes
            int hTemplateFile         // handle to template file
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern unsafe byte ReadFile
        (
            SafeFileHandle hFile,      // handle to file
            void* pBuffer,            // data buffer
            int NumberOfBytesToRead,  // number of bytes to read
            int* pNumberOfBytesRead,  // number of bytes read
            int Overlapped            // overlapped buffer
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern unsafe byte ReadFileEx
        (
            SafeFileHandle hFile,      // handle to file
            void* pBuffer,            // data buffer
            int NumberOfBytesToRead,  // number of bytes to read
            int lpOverlapped,
            int lpCompletionRoutine

        );

        [DllImport("kernel32", SetLastError = true)]
        public static extern unsafe bool CloseHandle
        (
            System.IntPtr hObject // handle to object
        );

        [DllImport("kernel32.dll")]
        public static extern unsafe UInt32 GetLastError();

        [StructLayout(LayoutKind.Sequential)]
        public struct Offset
        {
            public UInt32 offset;
            public UInt32 offsetHigh;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct OverLapped
        {
            [FieldOffset(0)]
            public UInt32 Internal;

            [FieldOffset(4)]
            public UInt32 InternalHigh;

            [FieldOffset(8)]
            public Offset offset;

            [FieldOffset(8)]
            public IntPtr pointer;

            [FieldOffset(16)]
            public IntPtr handle;
        }

        public static SafeFileHandle Open(string FileName)
        {
            // open the existing file for reading       
            
            SafeFileHandle handle = CreateFile
            (
                FileName,
                GENERIC_READ,
                FILE_SHARE_READ | FILE_SHARE_WRITE,
                0,
                OPEN_EXISTING,
                0,
                0
            );

            if (handle != null)
            {
                return handle;
            }
            else
            {
                return null;
            }
        }

        public static unsafe byte[] ReadFile(SafeFileHandle handle,byte[] buffer, int index, int count)
        {
            int n = 0;
            fixed (byte* p = buffer)
            {
                ReadFile(handle, p + index, count, &n, 0);
                return buffer;
            }    
        }
        //TODO: to change from file_Begin to file current for optimization;
        public static unsafe UInt32 SetFilePointer(SafeFileHandle handle, UInt64 byteAddress)
        {
            //power;
            Int32 lowAdd = (Int32)(byteAddress % UInt32.MaxValue);
            Int32 highAdd = (Int32)(byteAddress / UInt32.MaxValue);

           return SetFilePointer(handle, lowAdd, &highAdd, FILE_BEGIN);
        }


    }
}
