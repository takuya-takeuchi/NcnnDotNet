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
        public static extern int cpu_set_cpu_powersave(int powerSave);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int cpu_get_omp_num_threads();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void cpu_set_omp_num_threads(int numThreads);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int cpu_get_omp_dynamic();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void cpu_set_omp_dynamic(int dynamic);

    }

}