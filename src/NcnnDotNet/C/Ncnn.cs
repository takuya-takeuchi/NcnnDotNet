using System;
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