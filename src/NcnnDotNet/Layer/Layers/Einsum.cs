﻿using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.Layers
{

    public sealed class Einsum : Layer
    {

        #region Constructors

        public Einsum()
        {
            NativeMethods.layer_layers_Einsum_new(out var ret);
            this.NativePtr = ret;
        }

        #endregion

        #region Methods

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

            NativeMethods.layer_layers_Einsum_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}