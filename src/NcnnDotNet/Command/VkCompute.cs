using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class VkCompute : NcnnObject
    {

        #region Constructors

        public VkCompute(VulkanDevice device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            device.ThrowIfDisposed();

            NativeMethods.command_VkCompute_new(device.NativePtr, out var ret);
            this.NativePtr = ret;
        }

        #endregion

        #region Methods

        public void RecordDownload(VkMat mat)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            this.ThrowIfDisposed();
            mat.ThrowIfDisposed();

            var error = NativeMethods.command_VkCompute_record_download(this.NativePtr, mat.NativePtr);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");
        }

        public void RecordUpload(Mat mat, VkMat dst, Option option, bool flatten)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (dst == null)
                throw new ArgumentNullException(nameof(dst));
            if (option == null)
                throw new ArgumentNullException(nameof(option));

            this.ThrowIfDisposed();
            mat.ThrowIfDisposed();
            dst.ThrowIfDisposed();
            option.ThrowIfDisposed();

            var error = NativeMethods.command_VkCompute_record_upload(this.NativePtr,
                                                                      mat.NativePtr,
                                                                      dst.NativePtr,
                                                                      option.NativePtr,
                                                                      flatten);
            // public static extern ErrorType command_VkCompute_record_upload(IntPtr compute,
            //                                                                IntPtr mat,
            //                                                                IntPtr dst,
            //                                                                IntPtr opt,
            //                                                                bool flattern);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");
        }

        public int SubmitAndWait()
        {
            this.ThrowIfDisposed();

            var error = NativeMethods.command_VkCompute_submit_and_wait(this.NativePtr, out var returnValue);
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

            NativeMethods.command_VkCompute_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}