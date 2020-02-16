using System;
using System.Linq;
using System.Runtime.InteropServices;
using NcnnDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class ModelBinFromMatArray : ModelBin
    {
        
        #region Constructors

        public ModelBinFromMatArray(Mat[] weights)
        {
            if (weights == null) 
                throw new ArgumentNullException(nameof(weights));

            weights.ThrowIfDisposed();

            var array = weights.Select(mat => mat.NativePtr).ToArray();
            NativeMethods.modelbin_ModelBinFromMatArray_new(array, weights.Length, out var ret);
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