using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class VkBlobBufferAllocator : VkAllocator
    {

        #region Constructors

        public VkBlobBufferAllocator(VulkanDevice vulkanDevice)
        {
            if (vulkanDevice == null)
                throw new ArgumentNullException(nameof(vulkanDevice));

            vulkanDevice.ThrowIfDisposed();

            NativeMethods.allocator_VkBlobBufferAllocator_new(vulkanDevice.NativePtr, out var returnValue);
            this.NativePtr = returnValue;
        }

        internal VkBlobBufferAllocator(IntPtr ptr, bool isEnabledDispose = true) :
            base(ptr, isEnabledDispose)
        {
        }

        #endregion

        #region Methods

        public void Clear()
        {
            this.ThrowIfDisposed();

            NativeMethods.allocator_VkBlobBufferAllocator_clear(this.NativePtr);
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

            NativeMethods.allocator_VkBlobBufferAllocator_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}