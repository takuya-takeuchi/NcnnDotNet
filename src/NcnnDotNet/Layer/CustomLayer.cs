using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public abstract class CustomLayer : Layer
    {

        #region Delegates

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int OnForwardDelegate(IntPtr bottomBlob, IntPtr topBlob, IntPtr opt);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int OnForwardInplaceDelegate(IntPtr bottomBlob, IntPtr opt);

        #endregion

        #region Fields

        private readonly DelegateHandler<OnForwardDelegate> _Forward;

        private readonly DelegateHandler<OnForwardInplaceDelegate> _ForwardInplace;

        #endregion

        #region Constructors

        protected CustomLayer()
        {
            this._Forward = new DelegateHandler<OnForwardDelegate>(this.OnForward);
            this._ForwardInplace = new DelegateHandler<OnForwardInplaceDelegate>(this.OnForwardInplace);

            NativeMethods.layer_CustomLayer_new(out var ret,
                                                this._Forward.Handle,
                                                this._ForwardInplace.Handle);
            this.NativePtr = ret;
        }

        #endregion

        #region Methods

        public sealed override int Forward(Mat bottomBlob, Mat topBlob, Option opt)
        {
            return this.OnForward(bottomBlob, topBlob, opt);
        }

        public sealed override int ForwardInplace(Mat bottomBlob, Option opt)
        {
            return this.OnForwardInplace(bottomBlob, opt);
        }

        #region Overrides

        protected virtual int OnForward(Mat bottomBlob, Mat topBlob, Option opt)
        {
            return base.Forward(bottomBlob, topBlob, opt);
        }

        protected virtual int OnForwardInplace(Mat bottomBlob, Option opt)
        {
            return base.ForwardInplace(bottomBlob, opt);
        }

        #endregion

        #region Helpers

        private int OnForward(IntPtr bottomBlob, IntPtr topBlob, IntPtr opt)
        {
            return this.OnForward(new Mat(bottomBlob, false), new Mat(topBlob, false), opt == IntPtr.Zero ? null : new Option(opt, false));
        }

        private int OnForwardInplace(IntPtr bottomBlob, IntPtr opt)
        {
            return this.OnForwardInplace(new Mat(bottomBlob, false), opt == IntPtr.Zero ? null : new Option(opt, false));
        }

        #endregion

        #region Overrides 

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            // Suspend to call Layer.DisposeUnmanaged
            //base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.layer_CustomLayer_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}