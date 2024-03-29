﻿using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr datareader_DataReaderFromMemory_new(byte[] mem, uint length);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void datareader_DataReaderFromMemory_delete(IntPtr reader);

    }

}