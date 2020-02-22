using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace TestUtil
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType prng_prng_rand_t_new(out IntPtr returnValue);
        
        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void prng_prng_rand_t_delete(IntPtr state);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType prng_prng_rand(IntPtr state, out ulong returnValue);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType prng_prng_srand(ulong seed, IntPtr state);

    }

}