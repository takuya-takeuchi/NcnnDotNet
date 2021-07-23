using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class VkTransfer : NcnnObject
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