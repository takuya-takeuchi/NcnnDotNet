using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class VkStagingAllocator : VkAllocator
    {

        #region Constructors

        public VkStagingAllocator(VulkanDevice vulkanDevice)
        {
            if (vulkanDevice == null)
                throw new ArgumentNullException(nameof(vulkanDevice));

            vulkanDevice.ThrowIfDisposed();

            NativeMethods.allocator_VkStagingAllocator_new(vulkanDevice.NativePtr, out var returnValue);
            this.NativePtr = returnValue;
        }

        internal VkStagingAllocator(IntPtr ptr, bool isEnabledDispose = true) :
            base(ptr, isEnabledDispose)
        {
        }

        #endregion

        #region Methods

        public override void Clear()
        {
            this.ThrowIfDisposed();

            NativeMethods.allocator_VkStagingAllocator_clear(this.NativePtr);
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

            NativeMethods.allocator_VkStagingAllocator_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}