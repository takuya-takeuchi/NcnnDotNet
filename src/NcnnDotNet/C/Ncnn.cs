using System.Text;

namespace NcnnDotNet.C
{

    /// <summary>
    /// Provides the methods of ncnn.
    /// </summary>
    public static partial class Ncnn
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
            NativeMethods.c_ncnn_allocator_destroy(allocator.NativePtr);
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