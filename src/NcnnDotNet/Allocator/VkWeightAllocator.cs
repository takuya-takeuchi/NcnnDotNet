using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class VkWeightAllocator : VkAllocator
    {

        #region Constructors

        public VkWeightAllocator(VulkanDevice vulkanDevice)
        {
            if (vulkanDevice == null)
                throw new ArgumentNullException(nameof(vulkanDevice));

            vulkanDevice.ThrowIfDisposed();

            NativeMethods.allocator_VkWeightAllocator_new(vulkanDevice.NativePtr, out var returnValue);
            this.NativePtr = returnValue;
        }

        internal VkWeightAllocator(IntPtr ptr, bool isEnabledDispose = true) :
            base(ptr, isEnabledDispose)
        {
        }

        #endregion

        #region Methods

        public override void Clear()
        {
            this.ThrowIfDisposed();

            NativeMethods.allocator_VkWeightAllocator_clear(this.NativePtr);
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

            NativeMethods.allocator_VkWeightAllocator_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}