// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    /// <summary>
    /// Provides the methods of ncnn.
    /// </summary>
    public static partial class Ncnn
    {

        #region Methods

        public static void CastFloat16ToFloat32(Mat src, Mat dst, Option option = null)
        {
            var inputOption = option != null;
            var opt = inputOption ? option : new Option();

            try
            {
                var error = NativeMethods.mat_cast_float16_to_float32(src.NativePtr,
                                                                      dst.NativePtr,
                                                                      opt.NativePtr);
                if (error != NativeMethods.ErrorType.OK)
                    throw new NcnnException("Unknown Exception");
            }
            finally
            {
                if (!inputOption)
                    opt?.Dispose();
            }
        }

        public static void CastFloat32ToFloat16(Mat src, Mat dst, Option option = null)
        {
            var inputOption = option != null;
            var opt = inputOption ? option : new Option();

            try
            {
                var error = NativeMethods.mat_cast_float32_to_float16(src.NativePtr,
                                                                      dst.NativePtr,
                                                                      opt.NativePtr);
                if (error != NativeMethods.ErrorType.OK)
                    throw new NcnnException("Unknown Exception");
            }
            finally
            {
                if (!inputOption)
                    opt?.Dispose();
            }
        }

        public static void CopyMakeBorder(Mat src, Mat dst, int top, int bottom, int left, int right, BorderType type, float v, Option option = null)
        {
            var inputOption = option != null;
            var opt = inputOption ? option : new Option();

            try
            {
                var error = NativeMethods.mat_copy_make_border(src.NativePtr,
                                                               dst.NativePtr,
                                                               top,
                                                               bottom,
                                                               left,
                                                               right,
                                                               type,
                                                               v,
                                                               opt.NativePtr);
                if (error != NativeMethods.ErrorType.OK)
                    throw new NcnnException("Unknown Exception");
            }
            finally
            {
                if (!inputOption)
                    opt?.Dispose();
            }
        }

        public static void ConvertPacking(Mat src, Mat dst, int elemPack, Option option = null)
        {
            var inputOption = option != null;
            var opt = inputOption ? option : new Option();

            try
            {
                var error = NativeMethods.mat_convert_packing(src.NativePtr,
                                                              dst.NativePtr,
                                                              elemPack,
                                                              opt.NativePtr);
                if (error != NativeMethods.ErrorType.OK)
                    throw new NcnnException("Unknown Exception");
            }
            finally
            {
                if (!inputOption)
                    opt?.Dispose();
            }
        }

        public static void ResizeBilinear(Mat src, Mat dst, int width, int height, Option option = null)
        {
            var inputOption = option != null;
            var opt = inputOption ? option : new Option();

            try
            {
                var error = NativeMethods.mat_resize_bilinear(src.NativePtr,
                                                              dst.NativePtr,
                                                              width,
                                                              height,
                                                              opt.NativePtr);
                if (error != NativeMethods.ErrorType.OK)
                    throw new NcnnException("Unknown Exception");
            }
            finally
            {
                if (!inputOption)
                    opt?.Dispose();
            }
        }

        public static void ResizeBicubic(Mat src, Mat dst, int width, int height, Option option = null)
        {
            var inputOption = option != null;
            var opt = inputOption ? option : new Option();

            try
            {
                var error = NativeMethods.mat_resize_bicubic(src.NativePtr,
                                                             dst.NativePtr,
                                                             width,
                                                             height,
                                                             opt.NativePtr);
                if (error != NativeMethods.ErrorType.OK)
                    throw new NcnnException("Unknown Exception");
            }
            finally
            {
                if (!inputOption)
                    opt?.Dispose();
            }
        }

        #endregion

    }

}