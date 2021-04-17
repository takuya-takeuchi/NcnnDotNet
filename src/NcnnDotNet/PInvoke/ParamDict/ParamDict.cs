using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType paramdict_ParamDict_new(out IntPtr paramdict);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void paramdict_ParamDict_delete(IntPtr paramdict);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void paramdict_ParamDict_set(IntPtr paramdict, int id, int i);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void paramdict_ParamDict_set2(IntPtr paramdict, int id, float f);

    }

}