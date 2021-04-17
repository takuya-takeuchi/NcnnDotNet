using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class ModelBinFromMatArray : ModelBin
    {
        
        #region Constructors

        public ModelBinFromMatArray(StdVector<Mat> weights)
        {
            if (weights == null) 
                throw new ArgumentNullException(nameof(weights));

            weights.ThrowIfDisposed();

            NativeMethods.modelbin_ModelBinFromMatArray_new(weights.NativePtr, out var ret);
            this.NativePtr = ret;
        }

        #endregion

        #region Methods

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.modelbin_ModelBinFromMatArray_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}