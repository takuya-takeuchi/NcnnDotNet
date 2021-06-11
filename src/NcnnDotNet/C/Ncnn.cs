using System;
using System.Linq;
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

        #region Mat Pixel

        public static Mat MatFromPixels(byte[] pixels, PixelType type, int width, int height, int stride, Allocator allocator = null)
        {
            if (pixels == null)
                throw new ArgumentNullException(nameof(pixels));

            var mat = NativeMethods.c_ncnn_mat_from_pixels(pixels, type, width, height, stride, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(mat);
        }

        public static Mat MatFromPixelsResize(byte[] pixels, PixelType type, int width, int height, int stride, int targetWidth, int targetHeight, Allocator allocator = null)
        {
            if (pixels == null)
                throw new ArgumentNullException(nameof(pixels));

            var mat = NativeMethods.c_ncnn_mat_from_pixels_resize(pixels, type, width, height, stride, targetWidth, targetHeight, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(mat);
        }

        public static Mat MatFromPixelsRoi(byte[] pixels, PixelType type, int width, int height, int stride, int roiX, int roiY, int roiWidth, int roiHeight, Allocator allocator = null)
        {
            if (pixels == null)
                throw new ArgumentNullException(nameof(pixels));

            var mat = NativeMethods.c_ncnn_mat_from_pixels_roi(pixels, type, width, height, stride, roiX, roiY, roiWidth, roiHeight, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(mat);
        }

        public static Mat MatFromPixelsRoiResize(byte[] pixels, PixelType type, int width, int height, int stride, int roiX, int roiY, int roiWidth, int roiHeight, int targetWidth, int targetHeight, Allocator allocator = null)
        {
            if (pixels == null)
                throw new ArgumentNullException(nameof(pixels));

            var mat = NativeMethods.c_ncnn_mat_from_pixels_roi_resize(pixels, type, width, height, stride, roiX, roiY, roiWidth, roiHeight, targetWidth, targetHeight, allocator?.NativePtr ?? IntPtr.Zero);
            return new Mat(mat);
        }

        public static void MatToPixels(Mat mat, byte[] pixels, PixelType type, int stride)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (pixels == null)
                throw new ArgumentNullException(nameof(pixels));

            NativeMethods.c_ncnn_mat_to_pixels(mat.NativePtr, pixels, type, stride);
        }

        public static void MatToPixelsResize(Mat mat, byte[] pixels, PixelType type, int targetWidth, int targetHeight, int targetStride)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (pixels == null)
                throw new ArgumentNullException(nameof(pixels));

            NativeMethods.c_ncnn_mat_to_pixels_resize(mat.NativePtr, pixels, type, targetWidth, targetHeight, targetStride);
        }

        public static void MatSubstractMeanNormalize(Mat mat, float[] means, float[] normalize)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (means == null && normalize == null)
                throw new ArgumentException($"{nameof(means)} or {nameof(normalize)} is null");
            if (means != null && normalize != null && means.Length != normalize.Length)
                throw new ArgumentException($"{nameof(means)}.{nameof(means.Length)} and {nameof(normalize)}.{nameof(normalize.Length)} must be same value");

            NativeMethods.c_ncnn_mat_substract_mean_normalize(mat.NativePtr, means, normalize);
        }

        public static void ConvertPacking(Mat src, out Mat dst, int elemPack, Option option)
        {
            if (src == null)
                throw new ArgumentNullException(nameof(src));
            if (option == null)
                throw new ArgumentNullException(nameof(option));

            NativeMethods.c_ncnn_convert_packing(src.NativePtr, out var ret, elemPack, option.NativePtr);
            dst = new Mat(ret);
        }

        public static void Flatten(Mat src, out Mat dst, Option option)
        {
            if (src == null)
                throw new ArgumentNullException(nameof(src));
            if (option == null)
                throw new ArgumentNullException(nameof(option));

            NativeMethods.c_ncnn_flatten(src.NativePtr, out var ret, option.NativePtr);
            dst = new Mat(ret);
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

        #region ParamDict

        public static ParamDict ParamDictCreate()
        {
            var paramDict = NativeMethods.c_ncnn_paramdict_create();
            return new ParamDict(paramDict);
        }

        public static void ParamDictDestroy(ParamDict paramDict)
        {
            if (paramDict == null)
                throw new ArgumentNullException(nameof(paramDict));

            NativeMethods.c_ncnn_paramdict_destroy(paramDict.NativePtr);
        }

        public static ParamDictType ParamDictGetType(ParamDict paramDict, int id)
        {
            if (paramDict == null)
                throw new ArgumentNullException(nameof(paramDict));

            return (ParamDictType)NativeMethods.c_ncnn_paramdict_get_type(paramDict.NativePtr, id);
        }

        public static int ParamDictGetInt(ParamDict paramDict, int id, int defaultValue)
        {
            if (paramDict == null)
                throw new ArgumentNullException(nameof(paramDict));

            return NativeMethods.c_ncnn_paramdict_get_int(paramDict.NativePtr, id, defaultValue);
        }

        public static float ParamDictGetFloat(ParamDict paramDict, int id, float defaultValue)
        {
            if (paramDict == null)
                throw new ArgumentNullException(nameof(paramDict));

            return NativeMethods.c_ncnn_paramdict_get_float(paramDict.NativePtr, id, defaultValue);
        }

        public static Mat ParamDictGetArray(ParamDict paramDict, int id, Mat defaultValue)
        {
            if (paramDict == null)
                throw new ArgumentNullException(nameof(paramDict));
            if (defaultValue == null)
                throw new ArgumentNullException(nameof(defaultValue));

            var ret = NativeMethods.c_ncnn_paramdict_get_array(paramDict.NativePtr, id, defaultValue.NativePtr);
            return new Mat(ret);
        }

        public static void ParamDictSetInt(ParamDict paramDict, int id, int value)
        {
            if (paramDict == null)
                throw new ArgumentNullException(nameof(paramDict));

            NativeMethods.c_ncnn_paramdict_set_int(paramDict.NativePtr, id, value);
        }

        public static void ParamDictSetFloat(ParamDict paramDict, int id, float value)
        {
            if (paramDict == null)
                throw new ArgumentNullException(nameof(paramDict));

            NativeMethods.c_ncnn_paramdict_set_float(paramDict.NativePtr, id, value);
        }

        public static void ParamDictSetArray(ParamDict paramDict, int id, Mat value)
        {
            if (paramDict == null)
                throw new ArgumentNullException(nameof(paramDict));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            NativeMethods.c_ncnn_paramdict_set_array(paramDict.NativePtr, id, value.NativePtr);
        }

        #endregion

        #region DataReader

        public static DataReader DataReaderCreate()
        {
            var paramDict = NativeMethods.c_ncnn_datareader_create();
            return new DataReader(paramDict);
        }

        public static void DataReaderDestroy(DataReader dataReader)
        {
            if (dataReader == null)
                throw new ArgumentNullException(nameof(dataReader));

            NativeMethods.c_ncnn_datareader_destroy(dataReader.NativePtr);
        }

        // ToDo
        // [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        // public static extern ncnn_datareader_t c_ncnn_datareader_create_from_stdio(FILE* fp);

        public static DataReader DataReaderCreateFromMemory(byte[] memory)
        {
            if (memory == null) 
                throw new ArgumentNullException(nameof(memory));

            var paramDict = NativeMethods.c_ncnn_datareader_create_from_memory(memory);
            return new DataReader(paramDict);
        }

        #endregion

        #region ModelBin

        public static ModelBin ModelBinCreateFromDataReader(DataReader dataReader)
        {
            if (dataReader == null) 
                throw new ArgumentNullException(nameof(dataReader));

            var paramDict = NativeMethods.c_ncnn_modelbin_create_from_datareader(dataReader.NativePtr);
            return new ModelBin(paramDict);
        }

        public static ModelBin ModelBinCreateFromMatArray(Mat[] weights, int number)
        {
            if (weights == null)
                throw new ArgumentNullException(nameof(weights));

            if (weights.Any(mat => mat == null))
                throw new ArgumentException($"{nameof(weights)} contains null");
            if (weights.Any(mat => mat.NativePtr == IntPtr.Zero))
                throw new ArgumentException($"{nameof(weights)} contains {nameof(IntPtr)}.{nameof(IntPtr.Zero)} element");

            var tmp = weights.Select(mat => mat.NativePtr).ToArray();
            if (!(tmp.Length >= number))
                throw new ArgumentException($"{nameof(weights)}.{nameof(weights.Length)} must be more than {number}");

            var paramDict = NativeMethods.c_ncnn_modelbin_create_from_mat_array(tmp, number);
            return new ModelBin(paramDict);
        }

        public static void ModelBinDestroy(ModelBin modelBin)
        {
            if (modelBin == null)
                throw new ArgumentNullException(nameof(modelBin));

            NativeMethods.c_ncnn_modelbin_destroy(modelBin.NativePtr);
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