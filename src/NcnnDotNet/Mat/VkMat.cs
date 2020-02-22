using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class VkMat : NcnnObject
    {

        #region Constructors

        public VkMat()
        {
            var error = NativeMethods.mat_VkMat_new(out var net);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            this.NativePtr = net;
        }

        internal VkMat(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Methods

        public void CreateLike(Mat mat, VkAllocator allocator, VkAllocator stagingAllocator)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (allocator == null)
                throw new ArgumentNullException(nameof(allocator));
            if (stagingAllocator == null)
                throw new ArgumentNullException(nameof(stagingAllocator));

            this.ThrowIfDisposed();
            mat.ThrowIfDisposed();
            allocator.ThrowIfDisposed();
            stagingAllocator.ThrowIfDisposed();

            var error = NativeMethods.mat_VkMat_create_like_mat(this.NativePtr,
                                                                mat.NativePtr,
                                                                allocator.NativePtr,
                                                                stagingAllocator.NativePtr);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");
        }

        public void CreateLike(VkMat mat, VkAllocator allocator, VkAllocator stagingAllocator)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (allocator == null)
                throw new ArgumentNullException(nameof(allocator));
            if (stagingAllocator == null)
                throw new ArgumentNullException(nameof(stagingAllocator));

            this.ThrowIfDisposed();
            mat.ThrowIfDisposed();
            allocator.ThrowIfDisposed();
            stagingAllocator.ThrowIfDisposed();

            var error = NativeMethods.mat_VkMat_create_like_vkmat(this.NativePtr,
                                                                  mat.NativePtr,
                                                                  allocator.NativePtr,
                                                                  stagingAllocator.NativePtr);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");
        }

        public void DiscardStagingBuffer()
        {
            this.ThrowIfDisposed();

            var error = NativeMethods.mat_VkMat_discard_staging_buffer(this.NativePtr);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");
        }

        public void Download(Mat mat)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            this.ThrowIfDisposed();
            mat.ThrowIfDisposed();

            var error = NativeMethods.mat_VkMat_download(this.NativePtr, mat.NativePtr);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");
        }

        public void PrepareStagingBuffer()
        {
            this.ThrowIfDisposed();

            var error = NativeMethods.mat_VkMat_prepare_staging_buffer(this.NativePtr);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");
        }

        public void Upload(Mat mat)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            this.ThrowIfDisposed();
            mat.ThrowIfDisposed();

            var error = NativeMethods.mat_VkMat_upload(this.NativePtr, mat.NativePtr);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");
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

            NativeMethods.mat_VkMat_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}