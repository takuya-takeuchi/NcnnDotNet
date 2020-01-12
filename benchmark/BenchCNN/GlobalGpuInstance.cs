using System;
using NcnnDotNet;

namespace BenchCNN
{

    internal sealed class GlobalGpuInstance : IDisposable
    {

        #region Constructors

        public GlobalGpuInstance()
        {
            Ncnn.CreateGpuInstance();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance has been disposed.
        /// </summary>
        /// <returns>true if this instance has been disposed; otherwise, false.</returns>
        public bool IsDisposed
        {
            get;
            private set;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Releases all resources used by this <see cref="NcnnObject"/>.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            //GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by this <see cref="NcnnObject"/>.
        /// </summary>
        /// <param name="disposing">Indicate value whether <see cref="IDisposable.Dispose"/> method was called.</param>
        private void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            // pre-disposing
            {
                if (disposing)
                {
                    // managed dispose
                }

                // unmanaged dispose
            }

            this.IsDisposed = true;

            if (disposing)
            {
                // managed dispose
            }

            // unmanaged dispose
            Ncnn.DestroyGpuInstance();
        }

        #endregion

    }

}