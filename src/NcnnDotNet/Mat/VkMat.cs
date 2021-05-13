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

        public void CreateLike(Mat mat, VkAllocator allocator)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (allocator == null)
                throw new ArgumentNullException(nameof(allocator));

            this.ThrowIfDisposed();
            mat.ThrowIfDisposed();
            allocator.ThrowIfDisposed();

            var error = NativeMethods.mat_VkMat_create_like_mat(this.NativePtr,
                                                                mat.NativePtr,
                                                                allocator.NativePtr);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");
        }

        public void CreateLike(VkMat mat, VkAllocator allocator)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (allocator == null)
                throw new ArgumentNullException(nameof(allocator));

            this.ThrowIfDisposed();
            mat.ThrowIfDisposed();
            allocator.ThrowIfDisposed();

            var error = NativeMethods.mat_VkMat_create_like_vkmat(this.NativePtr,
                                                                  mat.NativePtr,
                                                                  allocator.NativePtr);
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