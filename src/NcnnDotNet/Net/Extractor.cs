using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class Extractor : NcnnObject
    {

        #region Constructors

        internal Extractor(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Methods

        public bool Extract(string blobName, Mat feat)
        {
            if (string.IsNullOrEmpty(blobName))
                throw new ArgumentNullException(nameof(blobName));

            if (feat == null)
                throw new ArgumentNullException(nameof(feat));

            feat.ThrowIfDisposed();
            this.ThrowIfDisposed();

            var str = Ncnn.Encoding.GetBytes(blobName);
            var error = NativeMethods.net_Extractor_extract(this.NativePtr, str, str.Length, feat.NativePtr);
            if (error != NativeMethods.ErrorType.OK)
                return false;

            return true;
        }

        public bool Input(string blobName, Mat @in)
        {
            if (string.IsNullOrEmpty(blobName))
                throw new ArgumentNullException(nameof(blobName));

            if (@in == null)
                throw new ArgumentNullException(nameof(@in));

            @in.ThrowIfDisposed();
            this.ThrowIfDisposed();

            var str = Ncnn.Encoding.GetBytes(blobName);
            var error = NativeMethods.net_Extractor_input(this.NativePtr, str, str.Length, @in.NativePtr);
            if (error != NativeMethods.ErrorType.OK)
                return false;

            return true;
        }

        public void SetLiteMode(bool enable)
        {
            NativeMethods.net_Extractor_set_light_mode(this.NativePtr, enable);
        }

        public void SetNumThreads(int numThreads)
        {
            NativeMethods.net_Extractor_set_num_threads(this.NativePtr, numThreads);
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

            NativeMethods.net_Extractor_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}