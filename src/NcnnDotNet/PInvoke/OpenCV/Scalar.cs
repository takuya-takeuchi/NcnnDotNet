using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        #region double

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr opencv_Scalar_double_new(double v0, double v1, double v2, double v3);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void opencv_Scalar_double_delete(IntPtr scalar);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double opencv_Scalar_double_operator(IntPtr scalar, int index);

        #endregion

    }

}