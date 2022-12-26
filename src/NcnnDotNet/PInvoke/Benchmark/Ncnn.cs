using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double benchmark_get_current_time();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr datareader_DataReaderFromEmpty_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void datareader_DataReaderFromEmpty_delete(IntPtr reader);

    }

}