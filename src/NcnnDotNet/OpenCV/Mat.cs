using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.OpenCV
{

    public sealed class Mat : NcnnObject, ICloneable
    {

        #region Constructors

        internal Mat(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public int Cols
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.opencv_Mat_get_cols(this.NativePtr);
            }
        }

        public IntPtr Data
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.opencv_Mat_get_data(this.NativePtr);
            }
        }

        public bool IsEmpty
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.opencv_Mat_empty(this.NativePtr);
            }
        }
        
        public int Rows
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.opencv_Mat_get_rows(this.NativePtr);
            }
        }

        #endregion

        #region Methods

        public int Channels()
        {
            this.ThrowIfDisposed();
            return NativeMethods.opencv_Mat_channels(this.NativePtr);
        }
        
        public Mat Clone()
        {
            this.ThrowIfDisposed();
            var ret = NativeMethods.opencv_Mat_clone(this.NativePtr);
            return new Mat(ret);
        }
        
        public IntPtr Ptr(int y)
        {
            this.ThrowIfDisposed();
            return NativeMethods.opencv_Mat_ptr(this.NativePtr, y);
        }

        public int Total()
        {
            this.ThrowIfDisposed();
            return NativeMethods.opencv_Mat_total(this.NativePtr);
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

            NativeMethods.opencv_Mat_delete(this.NativePtr);
        }

        #endregion

        #endregion

        #region ICloneable Implementations

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion

    }

}