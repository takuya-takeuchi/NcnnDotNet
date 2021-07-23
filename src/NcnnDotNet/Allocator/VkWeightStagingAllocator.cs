using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class VkWeightStagingAllocator : VkAllocator
    {

        #region Constructors

        public VkWeightStagingAllocator(VulkanDevice vulkanDevice)
        {
            if (vulkanDevice == null)
                throw new ArgumentNullException(nameof(vulkanDevice));

            vulkanDevice.ThrowIfDisposed();

            NativeMethods.allocator_VkWeightStagingAllocator_new(vulkanDevice.NativePtr, out var returnValue);
            this.NativePtr = returnValue;
        }

        internal VkWeightStagingAllocator(IntPtr ptr, bool isEnabledDispose = true) :
            base(ptr, isEnabledDispose)
        {
        }

        #endregion

        #region Methods

        public override void Clear()
        {
            this.ThrowIfDisposed();

            NativeMethods.allocator_VkWeightStagingAllocator_clear(this.NativePtr);
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

            NativeMethods.allocator_VkWeightStagingAllocator_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}