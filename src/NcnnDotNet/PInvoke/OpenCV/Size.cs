using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        #region int32_t

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr opencv_Size_int32_t_new(int width, int height);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void opencv_Size_int32_t_delete(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Size_int32_t_get_width(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Size_int32_t_get_height(IntPtr rect);

        #endregion

        #region int64_t

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr opencv_Size_int64_t_new(long width, long height);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void opencv_Size_int64_t_delete(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern long opencv_Size_int64_t_get_width(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern long opencv_Size_int64_t_get_height(IntPtr rect);

        #endregion

        #region float

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr opencv_Size_float_new(float width, float height);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void opencv_Size_float_delete(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Size_float_get_width(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Size_float_get_height(IntPtr rect);

        #endregion

        #region double

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr opencv_Size_double_new(double width, double height);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void opencv_Size_double_delete(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Size_double_get_width(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Size_double_get_height(IntPtr rect);

        #endregion

    }

}