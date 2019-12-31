using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        #region int32_t

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr opencv_Point_int32_t_new(int x, int y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void opencv_Point_int32_t_delete(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Point_int32_t_get_x(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Point_int32_t_get_y(IntPtr rect);

        #endregion

        #region int64_t

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr opencv_Point_int64_t_new(long x, long y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void opencv_Point_int64_t_delete(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern long opencv_Point_int64_t_get_x(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern long opencv_Point_int64_t_get_y(IntPtr rect);

        #endregion

        #region float

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr opencv_Point_float_new(float x, float y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void opencv_Point_float_delete(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Point_float_get_x(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Point_float_get_y(IntPtr rect);

        #endregion

        #region double

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr opencv_Point_double_new(double x, double y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void opencv_Point_double_delete(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Point_double_get_x(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Point_double_get_y(IntPtr rect);

        #endregion

    }

}