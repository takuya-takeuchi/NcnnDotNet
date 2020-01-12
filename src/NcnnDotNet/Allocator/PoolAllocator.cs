using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class PoolAllocator : Allocator
    {

        #region Constructors

        public PoolAllocator()
        {
            NativeMethods.allocator_PoolAllocator_new(out var returnValue);
            this.NativePtr = returnValue;
        }

        internal PoolAllocator(IntPtr ptr, bool isEnabledDispose = true) :
            base(ptr, isEnabledDispose)
        {
        }

        #endregion

        #region Methods

        public void Clear()
        {
            this.ThrowIfDisposed();

            NativeMethods.allocator_PoolAllocator_clear(this.NativePtr);
        }

        public void SetSizeCompareRatio(float sizeCompareRatio = 0.75f)
        {
            if (!(0.0f <= sizeCompareRatio))
                throw new ArgumentOutOfRangeException(nameof(sizeCompareRatio), $"{nameof(sizeCompareRatio)} is less than zero.");
            if (!(sizeCompareRatio <= 1.0f))
                throw new ArgumentOutOfRangeException(nameof(sizeCompareRatio), $"{nameof(sizeCompareRatio)} is greater than zero.");

            this.ThrowIfDisposed();
            
            NativeMethods.allocator_PoolAllocator_set_size_compare_ratio(this.NativePtr, sizeCompareRatio);
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

            NativeMethods.allocator_PoolAllocator_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}