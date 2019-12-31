using System;
using System.Runtime.InteropServices;

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

        public Mat(int w, long elemSize = 4u)
        {
            // ToDo: Provide allocator class
            var error = NativeMethods.mat_Mat_new2(w, elemSize, IntPtr.Zero, out var net);
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

        public int C
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.mat_Mat_get_c(this.NativePtr);
            }
        }

        public int Dims
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.mat_Mat_get_dims(this.NativePtr);
            }
        }

        public int ElemPack
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.mat_Mat_get_elempack(this.NativePtr);
            }
        }

        public long ElemSize
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.mat_Mat_get_elemsize(this.NativePtr);
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
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.mat_Mat_set_operator_indexer(this.NativePtr, index, value);
            }
        }

        #endregion

        #region Methods

        public Mat Channel(int c)
        {
            this.ThrowIfDisposed();

            var ret = NativeMethods.mat_Mat_channel(this.NativePtr, c);
            return new Mat(ret);
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

        public float[] Row(int y)
        {
            this.ThrowIfDisposed();

            var ret = NativeMethods.mat_Mat_row(this.NativePtr, y);
            var width = this.Width;

            var array = new float[width];
            Marshal.Copy(ret, array, 0, width);

            return array;
        }

        public void SubstractMeanNormalize(float[] meanVals, float[] normVals)
        {
            this.ThrowIfDisposed();
            NativeMethods.mat_Mat_substract_mean_normalize(this.NativePtr, meanVals, normVals);
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