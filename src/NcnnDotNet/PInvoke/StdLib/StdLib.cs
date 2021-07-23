using System;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        #region string

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr string_c_str(IntPtr str);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void string_delete(IntPtr str);

        #endregion

        #region vector
        
        #region int32

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_int32_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_int32_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_int32_new3([In] int[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_int32_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_int32_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_int32_delete(IntPtr vector);

        #endregion

        #region uint32

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_uint32_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_uint32_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_uint32_new3([In] uint[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_uint32_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_uint32_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_uint32_delete(IntPtr vector);

        #endregion

        #region float

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_float_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_float_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_float_new3([In] float[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_float_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_float_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_float_delete(IntPtr vector);

        #endregion

        #region double

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_double_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_double_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_double_new3([In] double[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_double_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_double_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_double_delete(IntPtr vector);

        #endregion

        #region Mat

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_Mat_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_Mat_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_Mat_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_Mat_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_Mat_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_Mat_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_Mat_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #region VkMat

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_VkMat_new1();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_VkMat_new2(IntPtr size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_VkMat_new3([In] IntPtr[] data, IntPtr dataLength);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_VkMat_getSize(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr stdvector_VkMat_getPointer(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_VkMat_delete(IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void stdvector_VkMat_copy(IntPtr vector, IntPtr[] dst);

        #endregion

        #endregion

    }

}