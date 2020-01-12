using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public abstract class VkAllocator : NcnnObject
    {

        #region Constructors

        protected VkAllocator()
        {
        }

        protected VkAllocator(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

    }

}