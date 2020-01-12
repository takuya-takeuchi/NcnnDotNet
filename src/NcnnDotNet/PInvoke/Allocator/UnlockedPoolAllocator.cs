using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType allocator_UnlockedPoolAllocator_new(out IntPtr returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void allocator_UnlockedPoolAllocator_delete(IntPtr allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void allocator_UnlockedPoolAllocator_clear(IntPtr allocator);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void allocator_UnlockedPoolAllocator_set_size_compare_ratio(IntPtr allocator, float scr);

    }

}