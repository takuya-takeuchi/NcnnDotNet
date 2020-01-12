using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class VkWeightBufferAllocator : VkAllocator
    {

        #region Constructors

        public VkWeightBufferAllocator(VulkanDevice vulkanDevice)
        {
            if (vulkanDevice == null)
                throw new ArgumentNullException(nameof(vulkanDevice));

            vulkanDevice.ThrowIfDisposed();

            NativeMethods.allocator_VkWeightBufferAllocator_new(vulkanDevice.NativePtr, out var returnValue);
            this.NativePtr = returnValue;
        }

        internal VkWeightBufferAllocator(IntPtr ptr):
            base(ptr)
        {
        }

        #endregion

        #region Methods

        public void Clear()
        {
            this.ThrowIfDisposed();

            NativeMethods.allocator_VkWeightBufferAllocator_clear(this.NativePtr);
        }

        #region Overrides 

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.allocator_VkWeightBufferAllocator_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}