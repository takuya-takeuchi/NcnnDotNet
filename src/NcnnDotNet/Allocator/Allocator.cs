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

        protected Allocator(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

    }

}