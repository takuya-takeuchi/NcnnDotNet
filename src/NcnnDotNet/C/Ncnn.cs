using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NcnnDotNet.C
{

    /// <summary>
    /// Provides the methods of ncnn.
    /// </summary>
    public static class Ncnn
    {

        #region Methods

        #region Allocator

        public static Allocator AllocatorCreatePoolAllocator()
        {
            var ret = NativeMethods.c_ncnn_allocator_create_pool_allocator();
            return new Allocator(ret);
        }

        public static Allocator AllocatorCreateUnlockedPoolAllocator()
        {
            var ret = NativeMethods.c_ncnn_allocator_create_unlocked_pool_allocator();
            return new Allocator(ret);
        }

        public static void AllocatorDestroy(Allocator allocator)
        {
            if (allocator == null)
                throw new ArgumentNullException(nameof(allocator));

            NativeMethods.c_ncnn_allocator_destroy(allocator.NativePtr);
        }

        #endregion

        #region Option

        public static Option OptionCreate()
        {
            var ret = NativeMethods.c_ncnn_option_create();
            return new Option(ret);
        }

        public static void OptionDestroy(Option option)
        {
            if (option == null)
                throw new ArgumentNullException(nameof(option));

            NativeMethods.c_ncnn_option_destroy(option.NativePtr);
        }

        public static int OptionGetNumThreads(Option option)
        {
            if (option == null)
                throw new ArgumentNullException(nameof(option));

            return NativeMethods.c_ncnn_option_get_num_threads(option.NativePtr);
        }

        public static void OptionSetNumThreads(Option option, int numThreads)
        {
            if (option == null)
                throw new ArgumentNullException(nameof(option));

            NativeMethods.c_ncnn_option_set_num_threads(option.NativePtr, numThreads);
        }

        public static bool OptionGetUseVulkanCompute(Option option)
        {
            if (option == null)
                throw new ArgumentNullException(nameof(option));

            return NativeMethods.c_ncnn_option_get_use_vulkan_compute(option.NativePtr) != 0;
        }

        public static void OptionSetUseVulkanCompute(Option option, bool useVulkanCompute)
        {
            if (option == null)
                throw new ArgumentNullException(nameof(option));

            NativeMethods.c_ncnn_option_set_use_vulkan_compute(option.NativePtr, useVulkanCompute ? 1 : 0);
        }

        #endregion

        #region Mat

        public static Mat MatCreate1D(int width, Allocator allocator = null)
        {
            var mat = NativeMethods.c_ncnn_mat_create_1d(width, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(mat);
        }

        public static Mat MatCreate2D(int width, int height, Allocator allocator = null)
        {
            var mat = NativeMethods.c_ncnn_mat_create_2d(width, height, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(mat);
        }

        public static Mat MatCreate3D(int width, int height, int channel, Allocator allocator = null)
        {
            var mat = NativeMethods.c_ncnn_mat_create_3d(width, height, channel, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(mat);
        }

        public static Mat MatCreateExternal1D(int width, IntPtr data, Allocator allocator = null)
        {
            var mat = NativeMethods.c_ncnn_mat_create_external_1d(width, data, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(mat);
        }

        public static Mat MatCreateExternal2D(int width, int height, IntPtr data, Allocator allocator = null)
        {
            var mat = NativeMethods.c_ncnn_mat_create_external_2d(width, height, data, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(mat);
        }

        public static Mat MatCreateExternal3D(int width, int height, int channel, IntPtr data, Allocator allocator = null)
        {
            var mat = NativeMethods.c_ncnn_mat_create_external_3d(width, height, channel, data, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(mat);
        }

        public static Mat MatCreate1DElem(int width, ulong elemSize, int elemPack, Allocator allocator = null)
        {
            var mat = NativeMethods.c_ncnn_mat_create_1d_elem(width, elemSize, elemPack, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(mat);
        }

        public static Mat MatCreate2DElem(int width, int height, ulong elemSize, int elemPack, Allocator allocator = null)
        {
            var mat = NativeMethods.c_ncnn_mat_create_2d_elem(width, height, elemSize, elemPack, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(mat);
        }

        public static Mat MatCreate3DElem(int width, int height, int channel, ulong elemSize, int elemPack, Allocator allocator = null)
        {
            var mat = NativeMethods.c_ncnn_mat_create_3d_elem(width, height, channel, elemSize, elemPack, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(mat);
        }

        public static Mat MatCreateExternal1DElem(int width, IntPtr data, ulong elemSize, int elemPack, Allocator allocator = null)
        {
            var mat = NativeMethods.c_ncnn_mat_create_external_1d_elem(width, data, elemSize, elemPack, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(mat);
        }

        public static Mat MatCreateExternal2DElem(int width, int height, IntPtr data, ulong elemSize, int elemPack, Allocator allocator = null)
        {
            var mat = NativeMethods.c_ncnn_mat_create_external_2d_elem(width, height, data, elemSize, elemPack, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(mat);
        }

        public static Mat MatCreateExternal3DElem(int width, int height, int channel, IntPtr data, ulong elemSize, int elemPack, Allocator allocator = null)
        {
            var mat = NativeMethods.c_ncnn_mat_create_external_3d_elem(width, height, channel, data, elemSize, elemPack, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(mat);
        }

        public static void MatDestroy(Mat mat)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            NativeMethods.c_ncnn_mat_destroy(mat.NativePtr);
        }

        public static void MatFillFloat(Mat mat, float value)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            NativeMethods.c_ncnn_mat_fill_float(mat.NativePtr, value);
        }

        public static Mat MatClone(Mat mat, Allocator allocator = null)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            var ret = NativeMethods.c_ncnn_mat_clone(mat.NativePtr, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(ret);
        }

        public static Mat MatReshape1D(Mat mat, int width, Allocator allocator = null)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            var ret = NativeMethods.c_ncnn_mat_reshape_1d(mat.NativePtr, width, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(ret);
        }

        public static Mat MatReshape2D(Mat mat, int width, int height, Allocator allocator = null)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            var ret = NativeMethods.c_ncnn_mat_reshape_2d(mat.NativePtr, width, height, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(ret);
        }

        public static Mat MatReshape3D(Mat mat, int width, int height, int channel, Allocator allocator = null)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            var ret = NativeMethods.c_ncnn_mat_reshape_3d(mat.NativePtr, width, height, channel, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(ret);
        }

        public static int MatGetDims(Mat mat)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            return NativeMethods.c_ncnn_mat_get_dims(mat.NativePtr);
        }

        public static int MatGetW(Mat mat)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            return NativeMethods.c_ncnn_mat_get_w(mat.NativePtr);
        }

        public static int MatGetH(Mat mat)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            return NativeMethods.c_ncnn_mat_get_h(mat.NativePtr);
        }

        public static int MatGetC(Mat mat)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            return NativeMethods.c_ncnn_mat_get_c(mat.NativePtr);
        }

        public static ulong MatGetElemSize(Mat mat)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            return NativeMethods.c_ncnn_mat_get_elemsize(mat.NativePtr);
        }

        public static int MatGetElemPack(Mat mat)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            return NativeMethods.c_ncnn_mat_get_elempack(mat.NativePtr);
        }

        public static ulong MatGetCStep(Mat mat)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            return NativeMethods.c_ncnn_mat_get_cstep(mat.NativePtr);
        }

        public static IntPtr MatGetData(Mat mat)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            return NativeMethods.c_ncnn_mat_get_data(mat.NativePtr);
        }

        public static IntPtr MatGetChannelData(Mat mat, int channel)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            return NativeMethods.c_ncnn_mat_get_channel_data(mat.NativePtr, channel);
        }

        #endregion

        #region Blob

        public static string BlobGetName(Blob blob)
        {
            if (blob == null)
                throw new ArgumentNullException(nameof(blob));

            var ret = NativeMethods.c_ncnn_blob_get_name(blob.NativePtr);
            return Marshal.PtrToStringAnsi(ret);
        }

        public static int BlobGetProducer(Blob blob)
        {
            if (blob == null)
                throw new ArgumentNullException(nameof(blob));

            return NativeMethods.c_ncnn_blob_get_producer(blob.NativePtr);
        }

        public static int BlobGetConsumer(Blob blob)
        {
            if (blob == null)
                throw new ArgumentNullException(nameof(blob));

            return NativeMethods.c_ncnn_blob_get_consumer(blob.NativePtr);
        }

        public static void BlobGetShape(Blob blob, out int dims, out int width, out int height, out int channel)
        {
            if (blob == null)
                throw new ArgumentNullException(nameof(blob));

            NativeMethods.c_ncnn_blob_get_shape(blob.NativePtr, out dims, out width, out height, out channel);
        }

        #endregion

        #endregion

        #region Properties

        private static Encoding _Encoding = Encoding.UTF8;

        public static Encoding Encoding
        {
            get => _Encoding;
            set => _Encoding = value ?? Encoding.UTF8;
        }

        public static bool IsSupportVulkan => NativeMethods.is_support_vulkan();

        #endregion

    }

}