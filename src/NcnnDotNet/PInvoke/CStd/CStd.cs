using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        #region cstd

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr cstd_memcpy(IntPtr dest, IntPtr src, int count);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern unsafe IntPtr cstd_memcpy(byte* dest, byte* src, int count);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void cstd_memset(IntPtr buf, byte ch, long count);

        #endregion

    }

}