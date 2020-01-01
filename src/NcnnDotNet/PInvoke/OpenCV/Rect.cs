using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        #region int32_t

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr opencv_Rect_int32_t_new(int x, int y, int width, int height);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void opencv_Rect_int32_t_delete(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Rect_int32_t_get_x(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Rect_int32_t_get_y(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Rect_int32_t_get_width(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Rect_int32_t_get_height(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Rect_int32_t_area(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr opencv_Rect_int32_t_operator_logical_and(IntPtr left, IntPtr right);

        #endregion

        #region float

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr opencv_Rect_float_new(float x, float y, float width, float height);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void opencv_Rect_float_delete(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Rect_float_get_x(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Rect_float_get_y(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Rect_float_get_width(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Rect_float_get_height(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int opencv_Rect_float_area(IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr opencv_Rect_float_operator_logical_and(IntPtr left, IntPtr right);

        #endregion

    }

}