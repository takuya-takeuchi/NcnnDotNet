using System;

namespace NcnnDotNet.Interop
{

    internal static class InteropHelper
    {

        public static void Copy(IntPtr ptrSource, uint[] dest, uint elements)
        {
            Copy(ptrSource, dest, 0, elements);
        }

        public static unsafe void Copy(IntPtr ptrSource, uint[] dest, int startIndex, uint elements)
        {
            fixed (uint* ptrDest = &dest[startIndex])
                NativeMethods.cstd_memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * sizeof(uint)));
        }

        public static void Copy(IntPtr ptrSource, ushort[] dest, uint elements)
        {
            Copy(ptrSource, dest, 0, elements);
        }

        public static unsafe void Copy(IntPtr ptrSource, ushort[] dest, int startIndex, uint elements)
        {
            fixed (ushort* ptrDest = &dest[startIndex])
                NativeMethods.cstd_memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * sizeof(ushort)));
        }

        public static void Copy(IntPtr ptrSource, sbyte[] dest, uint elements)
        {
            Copy(ptrSource, dest, 0, elements);
        }

        public static unsafe void Copy(IntPtr ptrSource, sbyte[] dest, int startIndex, uint elements)
        {
            fixed (sbyte* ptrDest = &dest[startIndex])
                NativeMethods.cstd_memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * sizeof(sbyte)));
        }

        public static void Copy(IntPtr ptrSource, ulong[] dest, uint elements)
        {
            Copy(ptrSource, dest, 0, elements);
        }

        public static unsafe void Copy(IntPtr ptrSource, ulong[] dest, int startIndex, uint elements)
        {
            fixed (ulong* ptrDest = &dest[startIndex])
                NativeMethods.cstd_memcpy((IntPtr)ptrDest, ptrSource, (int)(elements * sizeof(ulong)));
        }

        public static unsafe void Copy(uint[] source, IntPtr ptrDest, uint elements)
        {
            fixed (uint* ptrSource = &source[0])
                NativeMethods.cstd_memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * sizeof(uint)));
        }

        public static unsafe void Copy(ushort[] source, IntPtr ptrDest, uint elements)
        {
            fixed (ushort* ptrSource = &source[0])
                NativeMethods.cstd_memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * sizeof(ushort)));
        }

        public static unsafe void Copy(sbyte[] source, IntPtr ptrDest, uint elements)
        {
            fixed (sbyte* ptrSource = &source[0])
                NativeMethods.cstd_memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * sizeof(sbyte)));
        }

        public static unsafe void Copy(ulong[] source, IntPtr ptrDest, uint elements)
        {
            fixed (ulong* ptrSource = &source[0])
                NativeMethods.cstd_memcpy(ptrDest, (IntPtr)ptrSource, (int)(elements * sizeof(ulong)));
        }

    }

}
