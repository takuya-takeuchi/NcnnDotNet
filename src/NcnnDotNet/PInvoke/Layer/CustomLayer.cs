using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType layer_CustomLayer_new(out IntPtr returnValue,
                                                             IntPtr forward_function,
                                                             IntPtr forward_inplace_function);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void layer_CustomLayer_delete(IntPtr layer);

    }

}