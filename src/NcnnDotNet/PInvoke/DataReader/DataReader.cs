using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr datareader_DataReader_new(IntPtr constructorFunction,
                                                              IntPtr destructorFunction,
                                                              IntPtr scanFunction,
                                                              IntPtr readFunction);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void datareader_DataReader_delete(IntPtr reader);

    }

}