using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class Mat : NcnnObject
    {

        #region Constructors

        public Mat()
        {
            var error = NativeMethods.mat_Mat_new(out var net);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            this.NativePtr = net;
        }

        internal Mat(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public int Channel
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.mat_Mat_get_c(this.NativePtr);
            }
        }

        public int Height
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.mat_Mat_get_h(this.NativePtr);
            }
        }

        public bool IsEmpty
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.mat_Mat_empty(this.NativePtr);
            }
        }

        public int Width
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.mat_Mat_get_w(this.NativePtr);
            }
        }

        public float this[int index]
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.mat_Mat_get_operator_indexer(this.NativePtr, index, out var returnValue);
                return returnValue;
            }
        }

        #endregion

        #region Methods

        public void SubstractMeanNormalize(float[] meanVals, float[] normVals)
        {
            this.ThrowIfDisposed();
            NativeMethods.mat_Mat_substract_mean_normalize(this.NativePtr, meanVals, normVals);
        }

        public static Mat FromPixelsResize(IntPtr pixel, PixelType type, int width, int height, int targetWidth, int targetHeight)
        {
            return FromPixelsResize(pixel,
                                    type,
                                    width,
                                    height,
                                    targetWidth,
                                    targetHeight,
                                    IntPtr.Zero);
        }

        public static Mat FromPixelsResize(IntPtr pixel, PixelType type, int width, int height, int targetWidth, int targetHeight, IntPtr allocator)
        {
            if (pixel == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(pixel));

            NativeMethods.mat_Mat_from_pixels_resize(pixel,
                                                     type,
                                                     width,
                                                     height,
                                                     targetWidth,
                                                     targetHeight,
                                                     allocator,
                                                     out var returnValue);

            return new Mat(returnValue);
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

            NativeMethods.mat_Mat_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}