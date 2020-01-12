using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class VkStagingBufferAllocator : VkAllocator
    {

        #region Constructors

        public VkStagingBufferAllocator(VulkanDevice vulkanDevice)
        {
            if (vulkanDevice == null)
                throw new ArgumentNullException(nameof(vulkanDevice));

            vulkanDevice.ThrowIfDisposed();

            NativeMethods.allocator_VkStagingBufferAllocator_new(vulkanDevice.NativePtr, out var returnValue);
            this.NativePtr = returnValue;
        }

        internal VkStagingBufferAllocator(IntPtr ptr, bool isEnabledDispose = true) :
            base(ptr, isEnabledDispose)
        {
        }

        #endregion

        #region Methods

        public void Clear()
        {
            this.ThrowIfDisposed();

            NativeMethods.allocator_VkStagingBufferAllocator_clear(this.NativePtr);
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

            NativeMethods.allocator_VkStagingBufferAllocator_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}