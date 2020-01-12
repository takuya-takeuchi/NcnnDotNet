using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public abstract class Allocator : NcnnObject
    {

        #region Constructors

        protected Allocator()
        {
        }

        internal Allocator(IntPtr ptr, bool isEnabledDispose = true) :
            base(isEnabledDispose)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

    }

}