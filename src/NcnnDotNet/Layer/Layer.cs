using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class Layer : NcnnObject
    {

        #region Constructors

        internal Layer(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Methods

        public int ForwardInplace(Mat mat, Option option)
        {
            if (mat == null)
                throw new ArgumentNullException(nameof(mat));
            if (option == null)
                throw new ArgumentNullException(nameof(option));

            this.ThrowIfDisposed();
            mat.ThrowIfDisposed();
            option.ThrowIfDisposed();

            var error = NativeMethods.layer_Layer_forward_inplace(this.NativePtr,
                                                                  mat.NativePtr,
                                                                  option.NativePtr,
                                                                  out var returnValue);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return returnValue;
        }

        public int LoadParam(ParamDict paramDict)
        {
            if (paramDict == null)
                throw new ArgumentNullException(nameof(paramDict));

            this.ThrowIfDisposed();
            paramDict.ThrowIfDisposed();

            var error = NativeMethods.layer_Layer_load_param(this.NativePtr, paramDict.NativePtr, out var returnValue);
            if (error != NativeMethods.ErrorType.OK)
                throw new NcnnException("Unknown Exception");

            return returnValue;
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

            NativeMethods.layer_Layer_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}