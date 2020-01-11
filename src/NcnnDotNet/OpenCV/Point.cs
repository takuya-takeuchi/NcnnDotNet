using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.OpenCV
{

    public sealed class Point<T>
        where T : struct
    {

        #region Constructors

        public Point() :
            this(default, default)
        {
        }

        public Point(T x, T y)
        {
            this.X = x;
            this.Y = y;
        }

        internal Point(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            var bridge = CreateBridge();
            this.X = bridge.GetX(ptr);
            this.Y = bridge.GetY(ptr);
            bridge.Dispose(ptr);
        }

        #endregion

        #region Properties

        public T Y
        {
            get;
            set;
        }

        public T X
        {
            get;
            set;
        }

        #endregion

        #region Methods

        internal NcnnObject ToNative()
        {
            var bridge = CreateBridge();
            var ret = bridge.Create(this.X, this.Y);
            return new Native<T>(ret, bridge);
        }

        #region Helpers

        private static Bridge<T> CreateBridge()
        {
            if (PointTypeRepository.SupportTypes.TryGetValue(typeof(T), out var type))
            {
                switch (type)
                {
                    case PointTypeRepository.ElementTypes.Int32:
                        return new Int32Bridge() as Bridge<T>;
                    case PointTypeRepository.ElementTypes.Int64:
                        return new Int64Bridge() as Bridge<T>;
                    case PointTypeRepository.ElementTypes.Float:
                        return new FloatBridge() as Bridge<T>;
                    case PointTypeRepository.ElementTypes.Double:
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

            public abstract IntPtr Create(T x, T y);

            public abstract void Dispose(IntPtr ptr);

            public abstract T GetX(IntPtr ptr);

            public abstract T GetY(IntPtr ptr);

            #endregion

        }

        private sealed class Int32Bridge : Bridge<int>
        {

            #region Methods

            public override IntPtr Create(int x, int y)
            {
                return NativeMethods.opencv_Point_int32_t_new(x, y);
            }

            public override void Dispose(IntPtr ptr)
            {
                NativeMethods.opencv_Point_int32_t_delete(ptr);
            }

            public override int GetX(IntPtr ptr)
            {
                return NativeMethods.opencv_Point_int32_t_get_x(ptr);
            }

            public override int GetY(IntPtr ptr)
            {
                return NativeMethods.opencv_Point_int32_t_get_y(ptr);
            }

            #endregion

        }

        private sealed class Int64Bridge : Bridge<long>
        {

            #region Methods

            public override IntPtr Create(long x, long y)
            {
                return NativeMethods.opencv_Point_int64_t_new(x, y);
            }

            public override void Dispose(IntPtr ptr)
            {
                NativeMethods.opencv_Point_int64_t_delete(ptr);
            }

            public override long GetX(IntPtr ptr)
            {
                return NativeMethods.opencv_Point_int64_t_get_x(ptr);
            }

            public override long GetY(IntPtr ptr)
            {
                return NativeMethods.opencv_Point_int64_t_get_y(ptr);
            }

            #endregion

        }

        private sealed class FloatBridge : Bridge<float>
        {

            #region Methods

            public override IntPtr Create(float x, float y)
            {
                return NativeMethods.opencv_Point_float_new(x, y);
            }

            public override void Dispose(IntPtr ptr)
            {
                NativeMethods.opencv_Point_float_delete(ptr);
            }

            public override float GetX(IntPtr ptr)
            {
                return NativeMethods.opencv_Point_float_get_x(ptr);
            }

            public override float GetY(IntPtr ptr)
            {
                return NativeMethods.opencv_Point_float_get_y(ptr);
            }

            #endregion

        }

        private sealed class DoubleBridge : Bridge<double>
        {

            #region Methods

            public override IntPtr Create(double x, double y)
            {
                return NativeMethods.opencv_Point_double_new(x, y);
            }

            public override void Dispose(IntPtr ptr)
            {
                NativeMethods.opencv_Point_double_delete(ptr);
            }

            public override double GetX(IntPtr ptr)
            {
                return NativeMethods.opencv_Point_double_get_x(ptr);
            }

            public override double GetY(IntPtr ptr)
            {
                return NativeMethods.opencv_Point_double_get_y(ptr);
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