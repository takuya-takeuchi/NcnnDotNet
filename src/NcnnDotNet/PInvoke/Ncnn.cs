using System;
using System.Runtime.InteropServices;
using uint8_t = System.Byte;
using uint16_t = System.UInt16;
using uint32_t = System.UInt32;
using int64_t = System.Int64;
using int8_t = System.SByte;
using int16_t = System.Int16;
using int32_t = System.Int32;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal sealed partial class NativeMethods
    {

        internal enum ErrorType
        {

            OK = 0x00000000,

            #region General

            GeneralError = 0x76000000,

            GeneralFileIOError      = -(GeneralError | 0x00000001),

            GeneralFileImageLoad    = -(GeneralError | 0x00000002),

            GeneralSerialization    = -(GeneralError | 0x00000003),

            GeneralInvalidParameter = -(GeneralError | 0x00000004),

            GeneralNotSupport       = -(GeneralError | 0x00000005),

            #endregion

        }

        internal enum AllocatorType
        {
            UnlockedPoolAllocator = 0,
            PoolAllocator
        };

        internal enum VkAllocatorType
        {
            VkBlobBufferAllocator = 0,
            VkWeightBufferAllocator,
            VkStagingBufferAllocator,
            VkWeightStagingBufferAllocator
        };

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr get_version();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool is_support_vulkan();

    }

}