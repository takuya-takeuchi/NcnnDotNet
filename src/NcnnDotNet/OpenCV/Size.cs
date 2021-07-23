using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.OpenCV
{

    public sealed class Size<T>
        where T : struct
    {

        #region Constructors

        public Size(T width, T height)
        {
            this.Width = width;
            this.Height = height;
        }

        internal Size(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            var bridge = CreateBridge();
            this.Width = bridge.GetWidth(ptr);
            this.Height = bridge.GetHeight(ptr);
            bridge.Dispose(ptr);
        }

        #endregion

        #region Properties

        public T Height
        {
            get;
            set;
        }

        public T Width
        {
            get;
            set;
        }

        #endregion

        #region Methods

        internal NcnnObject ToNative()
        {
            var bridge = CreateBridge();
            var ret = bridge.Create( this.Width, this.Height);
            return new Native<T>(ret, bridge);
        }

        #region Helpers

        private static Bridge<T> CreateBridge()
        {
            if (SizeTypeRepository.SupportTypes.TryGetValue(typeof(T), out var type))
            {
                switch (type)
                {
                    case SizeTypeRepository.ElementTypes.Int32:
                        return new Int32Bridge() as Bridge<T>;
                    case SizeTypeRepository.ElementTypes.Int64:
                        return new Int64Bridge() as Bridge<T>;
                    case SizeTypeRepository.ElementTypes.Float:
                        return new FloatBridge() as Bridge<T>;
                    case SizeTypeRepository.ElementTypes.Double:
                        return new DoubleBridge() as Bridge<T>;
                }
            }

            throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        #endregion

        #endregion

        #region Bridge

        private abstract class Bridge<T>
        {

            #region Methods

            public abstract IntPtr Create(T width, T height);

            public abstract void Dispose(IntPtr ptr);

            public abstract T GetWidth(IntPtr ptr);

            public abstract T GetHeight(IntPtr ptr);

            #endregion

        }

        private sealed class Int32Bridge : Bridge<int>
        {

            #region Methods

            public override IntPtr Create(int width, int height)
            {
                return NativeMethods.opencv_Size_int32_t_new(width, height);
            }

            public override void Dispose(IntPtr ptr)
            {
                NativeMethods.opencv_Size_int32_t_delete(ptr);
            }

            public override int GetWidth(IntPtr ptr)
            {
                return NativeMethods.opencv_Size_int32_t_get_width(ptr);
            }

            public override int GetHeight(IntPtr ptr)
            {
                return NativeMethods.opencv_Size_int32_t_get_height(ptr);
            }

            #endregion

        }

        private sealed class Int64Bridge : Bridge<long>
        {

            #region Methods

            public override IntPtr Create(long width, long height)
            {
                return NativeMethods.opencv_Size_int64_t_new(width, height);
            }

            public override void Dispose(IntPtr ptr)
            {
                NativeMethods.opencv_Size_int64_t_delete(ptr);
            }

            public override long GetWidth(IntPtr ptr)
            {
                return NativeMethods.opencv_Size_int64_t_get_width(ptr);
            }

            public override long GetHeight(IntPtr ptr)
            {
                return NativeMethods.opencv_Size_int64_t_get_height(ptr);
            }

            #endregion

        }

        private sealed class FloatBridge : Bridge<float>
        {

            #region Methods

            public override IntPtr Create(float width, float height)
            {
                return NativeMethods.opencv_Size_float_new(width, height);
            }

            public override void Dispose(IntPtr ptr)
            {
                NativeMethods.opencv_Size_float_delete(ptr);
            }

            public override float GetWidth(IntPtr ptr)
            {
                return NativeMethods.opencv_Size_float_get_width(ptr);
            }

            public override float GetHeight(IntPtr ptr)
            {
                return NativeMethods.opencv_Size_float_get_height(ptr);
            }

            #endregion

        }
        
        private sealed class DoubleBridge : Bridge<double>
        {

            #region Methods

            public override IntPtr Create(double width, double height)
            {
                return NativeMethods.opencv_Size_double_new(width, height);
            }

            public override void Dispose(IntPtr ptr)
            {
                NativeMethods.opencv_Size_double_delete(ptr);
            }

            public override double GetWidth(IntPtr ptr)
            {
                return NativeMethods.opencv_Size_double_get_width(ptr);
            }

            public override double GetHeight(IntPtr ptr)
            {
                return NativeMethods.opencv_Size_double_get_height(ptr);
            }

            #endregion

        }
        
        private sealed class Native<T> : NcnnObject
        {

            #region Fields

            private readonly Bridge<T> _Bridge;

            #endregion

            #region Constructors

            public Native(IntPtr ptr, Bridge<T> bridge)
            {
                this.NativePtr = ptr;
                this._Bridge = bridge;
            }

            #endregion

            #region Methods

            #region Overrides 

            /// <summary>
            /// Releases all unmanaged resources.
            /// </summary>
            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                this._Bridge.Dispose(this.NativePtr);
            }

            #endregion

            #endregion

        }

        #endregion

    }

}