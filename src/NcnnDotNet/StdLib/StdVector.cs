using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class StdVector<TItem> : NcnnObject, IList<TItem>
    {

        #region Fields
        
        private readonly Bridge<TItem> _Bridge;

        #endregion

        #region Constructors
        
        public StdVector()
        {
            this._Bridge = CreateBridge();
            this.NativePtr = this._Bridge.Create();
        }

        public StdVector(int size)
        {
            this._Bridge = CreateBridge();
            this.NativePtr = this._Bridge.Create(size);
        }

        public StdVector(IEnumerable<TItem> data)
        {
            this._Bridge = CreateBridge();
            this.NativePtr = this._Bridge.Create(data);
        }

        internal StdVector(IntPtr ptr)
        {
            this._Bridge = CreateBridge();
            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public IntPtr ElementPtr
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Bridge.GetElementPtr(this.NativePtr);
            }
        }

        public int Size
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Bridge.GetSize(this.NativePtr);
            }
        }

        #endregion

        #region Methods

        public TItem[] ToArray()
        {
            this.ThrowIfDisposed();
            return this._Bridge.ToArray(this.NativePtr);
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

            this._Bridge?.Dispose(this.NativePtr);
        }

        #endregion

        #region Helpers

        private static Bridge<TItem> CreateBridge()
        {
            if (StdVectorTypeRepository.SupportTypes.TryGetValue(typeof(TItem), out var type))
            {
                switch (type)
                {
                    case StdVectorTypeRepository.ElementTypes.Int32:
                        return new Int32Bridge() as Bridge<TItem>;
                    case StdVectorTypeRepository.ElementTypes.UInt32:
                        return new UInt32Bridge() as Bridge<TItem>;
                    case StdVectorTypeRepository.ElementTypes.Float:
                        return new FloatBridge() as Bridge<TItem>;
                    case StdVectorTypeRepository.ElementTypes.Double:
                        return new DoubleBridge() as Bridge<TItem>;
                    case StdVectorTypeRepository.ElementTypes.Mat:
                        return new MatBridge() as Bridge<TItem>;
                    case StdVectorTypeRepository.ElementTypes.VkMat:
                        return new VkMatBridge() as Bridge<TItem>;
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

            public abstract void CopyTo(IntPtr ptr, T[] array, int arrayIndex);

            public abstract IntPtr Create();

            public abstract IntPtr Create(int size);

            public abstract IntPtr Create(IEnumerable<T> data);

            public abstract void Dispose(IntPtr ptr);

            public abstract IntPtr GetElementPtr(IntPtr ptr);

            public abstract int GetSize(IntPtr ptr);

            public abstract T[] ToArray(IntPtr ptr);

            #endregion

        }

        private sealed class Int32Bridge : Bridge<int>
        {

            #region Methods

            public override void CopyTo(IntPtr ptr, int[] array, int arrayIndex)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return;

                var elementPtr = this.GetElementPtr(ptr);
                Marshal.Copy(elementPtr, array, arrayIndex, size);
            }

            public override IntPtr Create()
            {
                return NativeMethods.stdvector_int32_new1();
            }

            public override IntPtr Create(int size)
            {
                if (size < 0)
                    throw new ArgumentOutOfRangeException(nameof(size));

                return NativeMethods.stdvector_int32_new2(new IntPtr(size));
            }

            public override IntPtr Create(IEnumerable<int> data)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var array = data.ToArray();
                return NativeMethods.stdvector_int32_new3(array, new IntPtr(array.Length));
            }

            public override void Dispose(IntPtr ptr)
            {
                NativeMethods.stdvector_int32_delete(ptr);
            }

            public override IntPtr GetElementPtr(IntPtr ptr)
            {
                return NativeMethods.stdvector_int32_getPointer(ptr);
            }

            public override int GetSize(IntPtr ptr)
            {
                return NativeMethods.stdvector_int32_getSize(ptr).ToInt32();
            }

            public override int[] ToArray(IntPtr ptr)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return new int[0];

                var dst = new int[size];
                var elementPtr = this.GetElementPtr(ptr);
                Marshal.Copy(elementPtr, dst, 0, dst.Length);
                return dst;
            }

            #endregion

        }

        private sealed class UInt32Bridge : Bridge<uint>
        {

            #region Methods

            public override void CopyTo(IntPtr ptr, uint[] array, int arrayIndex)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return;

                var elementPtr = this.GetElementPtr(ptr);
                Interop.InteropHelper.Copy(elementPtr, array, arrayIndex, (uint)size);
            }

            public override IntPtr Create()
            {
                return NativeMethods.stdvector_uint32_new1();
            }

            public override IntPtr Create(int size)
            {
                if (size < 0)
                    throw new ArgumentOutOfRangeException(nameof(size));

                return NativeMethods.stdvector_uint32_new2(new IntPtr(size));
            }

            public override IntPtr Create(IEnumerable<uint> data)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var array = data.ToArray();
                return NativeMethods.stdvector_uint32_new3(array, new IntPtr(array.Length));
            }

            public override void Dispose(IntPtr ptr)
            {
                NativeMethods.stdvector_uint32_delete(ptr);
            }

            public override IntPtr GetElementPtr(IntPtr ptr)
            {
                return NativeMethods.stdvector_uint32_getPointer(ptr);
            }

            public override int GetSize(IntPtr ptr)
            {
                return NativeMethods.stdvector_uint32_getSize(ptr).ToInt32();
            }

            public override uint[] ToArray(IntPtr ptr)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return new uint[0];

                var dst = new uint[size];
                var elementPtr = this.GetElementPtr(ptr);
                Interop.InteropHelper.Copy(elementPtr, dst, (uint)dst.Length);
                return dst;
            }

            #endregion

        }

        private sealed class FloatBridge : Bridge<float>
        {

            #region Methods

            public override void CopyTo(IntPtr ptr, float[] array, int arrayIndex)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return;

                var elementPtr = this.GetElementPtr(ptr);
                Marshal.Copy(elementPtr, array, arrayIndex, size);
            }

            public override IntPtr Create()
            {
                return NativeMethods.stdvector_float_new1();
            }

            public override IntPtr Create(int size)
            {
                if (size < 0)
                    throw new ArgumentOutOfRangeException(nameof(size));

                return NativeMethods.stdvector_float_new2(new IntPtr(size));
            }

            public override IntPtr Create(IEnumerable<float> data)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var array = data.ToArray();
                return NativeMethods.stdvector_float_new3(array, new IntPtr(array.Length));
            }

            public override void Dispose(IntPtr ptr)
            {
                NativeMethods.stdvector_float_delete(ptr);
            }

            public override IntPtr GetElementPtr(IntPtr ptr)
            {
                return NativeMethods.stdvector_float_getPointer(ptr);
            }

            public override int GetSize(IntPtr ptr)
            {
                return NativeMethods.stdvector_float_getSize(ptr).ToInt32();
            }

            public override float[] ToArray(IntPtr ptr)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return new float[0];

                var dst = new float[size];
                var elementPtr = this.GetElementPtr(ptr);
                Marshal.Copy(elementPtr, dst, 0, dst.Length);
                return dst;
            }

            #endregion

        }

        private sealed class DoubleBridge : Bridge<double>
        {

            #region Methods

            public override void CopyTo(IntPtr ptr, double[] array, int arrayIndex)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return;

                var elementPtr = this.GetElementPtr(ptr);
                Marshal.Copy(elementPtr, array, arrayIndex, size);
            }

            public override IntPtr Create()
            {
                return NativeMethods.stdvector_double_new1();
            }

            public override IntPtr Create(int size)
            {
                if (size < 0)
                    throw new ArgumentOutOfRangeException(nameof(size));

                return NativeMethods.stdvector_double_new2(new IntPtr(size));
            }

            public override IntPtr Create(IEnumerable<double> data)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var array = data.ToArray();
                return NativeMethods.stdvector_double_new3(array, new IntPtr(array.Length));
            }

            public override void Dispose(IntPtr ptr)
            {
                NativeMethods.stdvector_double_delete(ptr);
            }

            public override IntPtr GetElementPtr(IntPtr ptr)
            {
                return NativeMethods.stdvector_double_getPointer(ptr);
            }

            public override int GetSize(IntPtr ptr)
            {
                return NativeMethods.stdvector_double_getSize(ptr).ToInt32();
            }

            public override double[] ToArray(IntPtr ptr)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return new double[0];

                var dst = new double[size];
                var elementPtr = this.GetElementPtr(ptr);
                Marshal.Copy(elementPtr, dst, 0, dst.Length);
                return dst;
            }

            #endregion

        }
        
        private sealed class MatBridge : Bridge<Mat>
        {

            #region Methods

            public override void CopyTo(IntPtr ptr, Mat[] array, int arrayIndex)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return;

                var dst = new IntPtr[size];
                NativeMethods.stdvector_Mat_copy(ptr, dst);
                var tmp = dst.Select(p => new Mat(p)).ToArray();
                Array.Copy(tmp, 0, array, arrayIndex, tmp.Length);
            }

            public override IntPtr Create()
            {
                return NativeMethods.stdvector_Mat_new1();
            }

            public override IntPtr Create(int size)
            {
                if (size < 0)
                    throw new ArgumentOutOfRangeException(nameof(size));

                return NativeMethods.stdvector_Mat_new2(new IntPtr(size));
            }

            public override IntPtr Create(IEnumerable<Mat> data)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var array = data.Select(rectangle => rectangle.NativePtr).ToArray();
                return NativeMethods.stdvector_Mat_new3(array, new IntPtr(array.Length));
            }

            public override void Dispose(IntPtr ptr)
            {
                NativeMethods.stdvector_Mat_delete(ptr);
            }

            public override IntPtr GetElementPtr(IntPtr ptr)
            {
                return NativeMethods.stdvector_Mat_getPointer(ptr);
            }

            public override int GetSize(IntPtr ptr)
            {
                return NativeMethods.stdvector_Mat_getSize(ptr).ToInt32();
            }

            public override Mat[] ToArray(IntPtr ptr)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return new Mat[0];

                var dst = new IntPtr[size];
                NativeMethods.stdvector_Mat_copy(ptr, dst);
                return dst.Select(p => new Mat(p)).ToArray();
            }

            #endregion

        }
        
        private sealed class VkMatBridge : Bridge<VkMat>
        {

            #region Methods

            public override void CopyTo(IntPtr ptr, VkMat[] array, int arrayIndex)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return;

                var dst = new IntPtr[size];
                NativeMethods.stdvector_VkMat_copy(ptr, dst);
                var tmp = dst.Select(p => new VkMat(p)).ToArray();
                Array.Copy(tmp, 0, array, arrayIndex, tmp.Length);
            }

            public override IntPtr Create()
            {
                return NativeMethods.stdvector_VkMat_new1();
            }

            public override IntPtr Create(int size)
            {
                if (size < 0)
                    throw new ArgumentOutOfRangeException(nameof(size));

                return NativeMethods.stdvector_VkMat_new2(new IntPtr(size));
            }

            public override IntPtr Create(IEnumerable<VkMat> data)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var array = data.Select(rectangle => rectangle.NativePtr).ToArray();
                return NativeMethods.stdvector_VkMat_new3(array, new IntPtr(array.Length));
            }

            public override void Dispose(IntPtr ptr)
            {
                NativeMethods.stdvector_VkMat_delete(ptr);
            }

            public override IntPtr GetElementPtr(IntPtr ptr)
            {
                return NativeMethods.stdvector_VkMat_getPointer(ptr);
            }

            public override int GetSize(IntPtr ptr)
            {
                return NativeMethods.stdvector_VkMat_getSize(ptr).ToInt32();
            }

            public override VkMat[] ToArray(IntPtr ptr)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return new VkMat[0];

                var dst = new IntPtr[size];
                NativeMethods.stdvector_VkMat_copy(ptr, dst);
                return dst.Select(p => new VkMat(p)).ToArray();
            }

            #endregion

        }

        #endregion

        #region IEnumerable<TItem> Members

        public IEnumerator<TItem> GetEnumerator()
        {
            return ((IEnumerable<TItem>)this.ToArray()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IList<TItem> Members

        public void Add(TItem item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(TItem item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(TItem[] array, int arrayIndex)
        {
            if (array == null) 
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), $"{nameof(arrayIndex)} is less than 0.");
            var size = array.Length - arrayIndex;
            if (size > this.Size)
                throw new ArgumentException($"The number of elements in the source StdVector<T> is greater than the available space from {nameof(arrayIndex)} to the end of the destination array.");

            this.ThrowIfDisposed();
            this._Bridge.CopyTo(this.NativePtr, array, arrayIndex);
        }

        public bool Remove(TItem item)
        {
            throw new NotImplementedException();
        }

        public int Count => this.Size;

        public bool IsReadOnly => false;

        public int IndexOf(TItem item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, TItem item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public TItem this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        #endregion

    }

}