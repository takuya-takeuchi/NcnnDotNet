using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class VkTransfer : Command
    {

        #region Constructors

        public VkTransfer(VulkanDevice device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            device.ThrowIfDisposed();

            NativeMethods.command_VkTransfer_new(device.NativePtr, out var ret);
            this.NativePtr = ret;
        }

        #endregion

        #region Properties

        public VkAllocator StagingVkAllocator
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.command_VkTransfer_get_staging_vkallocator(this.NativePtr, out var value);

                if (NativeMethods.allocator_VkAllocator_dynamic_cast(value, out var type))
                {
                    return VkAllocator.GetAllocator(value, type);
                }

                return null;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.command_VkTransfer_set_staging_vkallocator(this.NativePtr, value.NativePtr);
            }
        }

        public VkAllocator WeightVkAllocator
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.command_VkTransfer_get_weight_vkallocator(this.NativePtr, out var value);

                if (NativeMethods.allocator_VkAllocator_dynamic_cast(value, out var type))
                {
                    return VkAllocator.GetAllocator(value, type);
                }

                return null;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.command_VkTransfer_set_weight_vkallocator(this.NativePtr, value.NativePtr);
            }
        }

        #endregion

        #region Methods

        public int SubmitAndWait()
        {
            this.ThrowIfDisposed();

            var error = NativeMethods.command_VkTransfer_submit_and_wait(this.NativePtr, out var returnValue);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return returnValue;
        }

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            // Suspend to call Command.DisposeUnmanaged
            //base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.command_VkTransfer_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}