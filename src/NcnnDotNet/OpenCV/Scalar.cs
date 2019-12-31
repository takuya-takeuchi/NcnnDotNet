using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.OpenCV
{

    public sealed class Scalar<T>
        where T : struct
    {

        #region Constructors

        public Scalar(T val0 = default(T), T val1 = default(T), T val2 = default(T), T val3 = default(T))
        {
            this.Val0 = val0;
            this.Val1 = val1;
            this.Val2 = val2;
            this.Val3 = val3;
        }

        internal Scalar(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            var bridge = CreateBridge();
            this.Val0 = bridge.Operator(ptr, 0);
            this.Val1 = bridge.Operator(ptr, 1);
            this.Val2 = bridge.Operator(ptr, 2);
            this.Val3 = bridge.Operator(ptr, 3);
            bridge.Dispose(ptr);
        }

        #endregion

        #region Properties

        public T Val0
        {
            get;
            set;
        }

        public T Val1
        {
            get;
            set;
        }

        public T Val2
        {
            get;
            set;
        }

        public T Val3
        {
            get;
            set;
        }

        #endregion

        #region Methods

        internal NcnnObject ToNative()
        {
            var bridge = CreateBridge();
            var ret = bridge.Create(this.Val0, this.Val1, this.Val2, this.Val3);
            return new Native<T>(ret, bridge);
        }

        #region Helpers

        private static Bridge<T> CreateBridge()
        {
            if (ScalarTypeRepository.SupportTypes.TryGetValue(typeof(T), out var type))
            {
                switch (type)
                {
                    case ScalarTypeRepository.ElementTypes.Double:
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

            public abstract IntPtr Create(T v0, T v1, T v2, T v3);

            public abstract void Dispose(IntPtr ptr);

            public abstract T Operator(IntPtr ptr, int index);

            #endregion

        }

        private sealed class DoubleBridge : Bridge<double>
        {

            #region Methods

            public override IntPtr Create(double v0, double v1, double v2, double v3)
            {
                return NativeMethods.opencv_Scalar_double_new(v0, v1, v2, v3);
            }

            public override void Dispose(IntPtr ptr)
            {
                NativeMethods.opencv_Scalar_double_delete(ptr);
            }

            public override double Operator(IntPtr ptr, int index)
            {
                return NativeMethods.opencv_Scalar_double_operator(ptr, index);
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