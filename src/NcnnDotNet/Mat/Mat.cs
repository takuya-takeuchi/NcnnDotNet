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

        public Mat(int w, ulong elemSize = 4u, Allocator allocator = null)
        {
            allocator?.ThrowIfDisposed();

            var error = NativeMethods.mat_Mat_new2(w, elemSize, allocator?.NativePtr ?? IntPtr.Zero, out var net);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            this.NativePtr = net;
        }

        public Mat(int w, int h, ulong elemSize = 4u, Allocator allocator = null)
        {
            allocator?.ThrowIfDisposed();

            var error = NativeMethods.mat_Mat_new3(w, h, elemSize, allocator?.NativePtr ?? IntPtr.Zero, out var net);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            this.NativePtr = net;
        }

        public Mat(int w, int h, int c, ulong elemSize = 4u, Allocator allocator = null)
        {
            allocator?.ThrowIfDisposed();

            var error = NativeMethods.mat_Mat_new4(w, h, c, elemSize, allocator?.NativePtr ?? IntPtr.Zero, out var net);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            this.NativePtr = net;
        }

        public Mat(int w, byte[] data, ulong elemSize = 4u, Allocator allocator = null)
        {
            if (data == null) 
                throw new ArgumentNullException(nameof(data));

            allocator?.ThrowIfDisposed();

            var error = NativeMethods.mat_Mat_new5(w, data, elemSize, allocator?.NativePtr ?? IntPtr.Zero, out var net);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            this.NativePtr = net;
        }

        public Mat(int w, IntPtr data, ulong elemSize = 4u, Allocator allocator = null)
        {
            if (data == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(data));

            allocator?.ThrowIfDisposed();

            var error = NativeMethods.mat_Mat_new5(w, data, elemSize, allocator?.NativePtr ?? IntPtr.Zero, out var net);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            this.NativePtr = net;
        }

        public Mat(int w, int h, byte[] data, ulong elemSize = 4u, Allocator allocator = null)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            allocator?.ThrowIfDisposed();

            var error = NativeMethods.mat_Mat_new6(w, h, data, elemSize, allocator?.NativePtr ?? IntPtr.Zero, out var net);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            this.NativePtr = net;
        }

        public Mat(int w, int h, IntPtr data, ulong elemSize = 4u, Allocator allocator = null)
        {
            if (data == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(data));

            allocator?.ThrowIfDisposed();

            var error = NativeMethods.mat_Mat_new6(w, h, data, elemSize, allocator?.NativePtr ?? IntPtr.Zero, out var net);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            this.NativePtr = net;
        }

        public Mat(int w, int h, int c, byte[] data, ulong elemSize = 4u, Allocator allocator = null)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            allocator?.ThrowIfDisposed();

            var error = NativeMethods.mat_Mat_new7(w, h, c, data, elemSize, allocator?.NativePtr ?? IntPtr.Zero, out var net);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            this.NativePtr = net;
        }

        public Mat(int w, int h, int c, IntPtr data, ulong elemSize = 4u, Allocator allocator = null)
        {
            if (data == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(data));

            allocator?.ThrowIfDisposed();

            var error = NativeMethods.mat_Mat_new7(w, h, c, data, elemSize, allocator?.NativePtr ?? IntPtr.Zero, out var net);
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

        public IntPtr Data
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.mat_Mat_get_data(this.NativePtr);
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

        public int H
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

        public ulong Total
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.mat_Mat_total(this.NativePtr);
            }
        }

        public int W
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

        public float this[ulong index]
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.mat_Mat_get_operator_indexer2(this.NativePtr, index, out var returnValue);
                return returnValue;
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.mat_Mat_set_operator_indexer2(this.NativePtr, index, value);
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

        public Mat ChannelRange(int c, int channels)
        {
            this.ThrowIfDisposed();

            var ret = NativeMethods.mat_Mat_channel_range(this.NativePtr, c, channels);
            return new Mat(ret);
        }

        #region Create

        public void Create(int w, ulong elemSize = 4u, Allocator allocator = null)
        {
            this.ThrowIfDisposed();
            allocator?.ThrowIfDisposed();

            var ret = NativeMethods.mat_Mat_create(this.NativePtr, w, elemSize, allocator?.NativePtr ?? IntPtr.Zero);
        }

        public void Create(int w, int h, ulong elemSize = 4u, Allocator allocator = null)
        {
            this.ThrowIfDisposed();
            allocator?.ThrowIfDisposed();

            var ret = NativeMethods.mat_Mat_create2(this.NativePtr, w, h, elemSize, allocator?.NativePtr ?? IntPtr.Zero);
        }

        #endregion

        #region CreateLike
        
        public void CreateLike(Mat mat, Allocator allocator = null)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            this.ThrowIfDisposed();
            mat.ThrowIfDisposed();
            allocator?.ThrowIfDisposed();

            var error = NativeMethods.mat_Mat_create_like_mat(this.NativePtr,
                                                              mat.NativePtr,
                                                              allocator?.NativePtr ?? IntPtr.Zero);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");
        }

        public void CreateLike(VkMat mat, Allocator allocator = null)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));

            this.ThrowIfDisposed();
            mat.ThrowIfDisposed();
            allocator?.ThrowIfDisposed();

            var error = NativeMethods.mat_Mat_create_like_vkmat(this.NativePtr,
                                                                mat.NativePtr,
                                                                allocator?.NativePtr ?? IntPtr.Zero);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");
        }

        #endregion

        #region Fill

        public void Fill(float value)
        {
            this.ThrowIfDisposed();

            var error = NativeMethods.mat_Mat_fill_float(this.NativePtr, value);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");
        }

        public void Fill(int value)
        {
            this.ThrowIfDisposed();

            var error = NativeMethods.mat_Mat_fill_int(this.NativePtr, value);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");
        }

        #endregion

        #region FromPixels

        public static Mat FromPixels(IntPtr pixel, PixelType type, int width, int height, Allocator allocator = null)
        {
            if (pixel == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(pixel));
            allocator?.ThrowIfDisposed();

            NativeMethods.mat_Mat_from_pixels(pixel,
                                              type,
                                              width,
                                              height,
                                              allocator?.NativePtr ?? IntPtr.Zero,
                                              out var returnValue);

            return new Mat(returnValue);
        }

        public static Mat FromPixels(IntPtr pixel, PixelType type, int width, int height, int stride, Allocator allocator = null)
        {
            if (pixel == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(pixel));
            allocator?.ThrowIfDisposed();

            NativeMethods.mat_Mat_from_pixels2(pixel,
                                               type,
                                               width,
                                               height,
                                               stride,
                                               allocator?.NativePtr ?? IntPtr.Zero,
                                               out var returnValue);

            return new Mat(returnValue);
        }

        #endregion

        #region FromPixelsResize

        public static Mat FromPixelsResize(IntPtr pixel, PixelType type, int width, int height, int targetWidth, int targetHeight, Allocator allocator = null)
        {
            if (pixel == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(pixel));
            allocator?.ThrowIfDisposed();

            NativeMethods.mat_Mat_from_pixels_resize(pixel,
                                                     type,
                                                     width,
                                                     height,
                                                     targetWidth,
                                                     targetHeight,
                                                     allocator?.NativePtr ?? IntPtr.Zero,
                                                     out var returnValue);

            return new Mat(returnValue);
        }

        public static Mat FromPixelsResize(IntPtr pixel, PixelType type, int width, int height, int stride, int targetWidth, int targetHeight, Allocator allocator = null)
        {
            if (pixel == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(pixel));
            allocator?.ThrowIfDisposed();

            NativeMethods.mat_Mat_from_pixels_resize2(pixel,
                                                      type,
                                                      width,
                                                      height,
                                                      stride,
                                                      targetWidth,
                                                      targetHeight,
                                                      allocator?.NativePtr ?? IntPtr.Zero,
                                                      out var returnValue);

            return new Mat(returnValue);
        }

        #endregion

        #region Reshape

        public Mat Reshape(int w, Allocator allocator = null)
        {
            this.ThrowIfDisposed();
            allocator?.ThrowIfDisposed();

            var error = NativeMethods.mat_Mat_reshape(this.NativePtr, w, allocator?.NativePtr ?? IntPtr.Zero, out var ret);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return new Mat(ret);
        }

        public Mat Reshape(int w, int h, Allocator allocator = null)
        {
            this.ThrowIfDisposed();
            allocator?.ThrowIfDisposed();

            var error = NativeMethods.mat_Mat_reshape2(this.NativePtr, w, h, allocator?.NativePtr ?? IntPtr.Zero, out var ret);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return new Mat(ret);
        }

        public Mat Reshape(int w, int h, int c, Allocator allocator = null)
        {
            this.ThrowIfDisposed();
            allocator?.ThrowIfDisposed();

            var error = NativeMethods.mat_Mat_reshape3(this.NativePtr, w, h, c, allocator?.NativePtr ?? IntPtr.Zero, out var ret);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return new Mat(ret);
        }

        #endregion

        public MemoryBuffer Row(int y)
        {
            this.ThrowIfDisposed();

            var ret = NativeMethods.mat_Mat_row(this.NativePtr, y);
            var width = this.W;

            return new MemoryBuffer(this, ret, width);
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

        public sealed class MemoryBuffer
        {

            #region Fields

            private readonly Mat _Parent;

            private readonly IntPtr _Ptr;

            private readonly int _Length;

            #endregion

            #region Constructors

            internal MemoryBuffer(Mat parent, IntPtr ptr, int length)
            {
                this._Parent = parent;
                this._Ptr = ptr;
                this._Length = length;
            }

            #endregion

            #region Properties

            public float this[int index]
            {
                get
                {
                    this._Parent.ThrowIfDisposed();
                    unsafe
                    {
                        var ptr = (float*)this._Ptr;
                        var dst = ptr + index;
                        return *dst;
                    }
                }
                set
                {
                    this._Parent.ThrowIfDisposed();
                    unsafe
                    {
                        var ptr = (float*)this._Ptr;
                        var dst = ptr + index;
                        dst[0] = value;
                    }
                }
            }

            #endregion

        }

    }

}