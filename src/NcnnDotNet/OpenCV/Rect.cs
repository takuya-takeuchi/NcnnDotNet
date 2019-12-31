using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.OpenCV
{

    public sealed class Rect<T>
        where T : struct
    {

        #region Constructors

        public Rect()
        {
        }

        public Rect(Point<T> point, Size<T> size)
        {
            this.X = point.X;
            this.Y = point.Y;
            this.Width = size.Width;
            this.Height = size.Height;
        }

        internal Rect(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            var bridge = CreateBridge();
            this.X = bridge.GetX(ptr);
            this.Y = bridge.GetY(ptr);
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

        public T X
        {
            get;
            set;
        }

        public T Y
        {
            get;
            set;
        }

        #endregion

        #region Methods

        internal NcnnObject ToNative()
        {
            var bridge = CreateBridge();
            var ret = bridge.Create(this.X, this.Y, this.Width, this.Height);
            return new Native<T>(ret, bridge);
        }

        #region Helpers

        private static Bridge<T> CreateBridge()
        {
            if (RectTypeRepository.SupportTypes.TryGetValue(typeof(T), out var type))
            {
                switch (type)
                {
                    case RectTypeRepository.ElementTypes.Int32:
                        return new Int32Bridge() as Bridge<T>;
                    case RectTypeRepository.ElementTypes.Float:
                        return new FloatBridge() as Bridge<T>;
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

            public abstract T Area(IntPtr ptr);

            public abstract IntPtr Create(T x, T y, T width, T height);

            public abstract void Dispose(IntPtr ptr);

            public abstract T GetX(IntPtr ptr);

            public abstract T GetY(IntPtr ptr);

            public abstract T GetWidth(IntPtr ptr);

            public abstract T GetHeight(IntPtr ptr);

            #endregion

        }

        private sealed class Int32Bridge : Bridge<int>
        {

            #region Methods

            public override int Area(IntPtr ptr)
            {
                return NativeMethods.opencv_Rect_int32_t_area(ptr);
            }

            public override IntPtr Create(int x, int y, int width, int height)
            {
                return NativeMethods.opencv_Rect_int32_t_new(x, y, width, height);
            }

            public override void Dispose(IntPtr ptr)
            {
                NativeMethods.opencv_Rect_int32_t_delete(ptr);
            }

            public override int GetX(IntPtr ptr)
            {
                return NativeMethods.opencv_Rect_int32_t_get_x(ptr);
            }

            public override int GetY(IntPtr ptr)
            {
                return NativeMethods.opencv_Rect_int32_t_get_y(ptr);
            }

            public override int GetWidth(IntPtr ptr)
            {
                return NativeMethods.opencv_Rect_int32_t_get_width(ptr);
            }

            public override int GetHeight(IntPtr ptr)
            {
                return NativeMethods.opencv_Rect_int32_t_get_height(ptr);
            }

            #endregion

        }

        private sealed class FloatBridge : Bridge<float>
        {

            #region Methods

            public override float Area(IntPtr ptr)
            {
                return NativeMethods.opencv_Rect_float_area(ptr);
            }

            public override IntPtr Create(float x, float y, float width, float height)
            {
                return NativeMethods.opencv_Rect_float_new(x, y, width, height);
            }

            public override void Dispose(IntPtr ptr)
            {
                NativeMethods.opencv_Rect_float_delete(ptr);
            }

            public override float GetX(IntPtr ptr)
            {
                return NativeMethods.opencv_Rect_float_get_x(ptr);
            }

            public override float GetY(IntPtr ptr)
            {
                return NativeMethods.opencv_Rect_float_get_y(ptr);
            }

            public override float GetWidth(IntPtr ptr)
            {
                return NativeMethods.opencv_Rect_float_get_width(ptr);
            }

            public override float GetHeight(IntPtr ptr)
            {
                return NativeMethods.opencv_Rect_float_get_height(ptr);
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