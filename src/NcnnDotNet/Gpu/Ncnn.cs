// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    /// <summary>
    /// Provides the methods of ncnn.
    /// </summary>
    public static partial class Ncnn
    {

        #region Methods

        public static int CreateGpuInstance()
        {
            return NativeMethods.gpu_create_gpu_instance();
        }

        public static void DestroyGpuInstance()
        {
            NativeMethods.gpu_destroy_gpu_instance();
        }

        #endregion

    }

}