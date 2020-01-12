using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int cpu_get_cpu_count();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int cpu_get_cpu_powersave();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int cpu_set_cpu_powersave(int powersave);

    }

}