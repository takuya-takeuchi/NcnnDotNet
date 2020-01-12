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

        public static int GetDefaultGpuIndex()
        {
            return NativeMethods.gpu_get_default_gpu_index();
        }

        public static int GetGpuCount()
        {
            return NativeMethods.gpu_get_gpu_count();
        }

        #endregion

    }

}