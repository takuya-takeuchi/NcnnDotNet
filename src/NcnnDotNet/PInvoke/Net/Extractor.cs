using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void net_Extractor_delete(IntPtr net);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType net_Extractor_input(IntPtr extractor, byte[] blobName, int blobNameLength, IntPtr @in);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType net_Extractor_extract(IntPtr extractor, byte[] blobName, int blobNameLength, IntPtr feat);

    }

}