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

        internal VkAllocator(IntPtr ptr, bool isEnabledDispose = true) :
            base(isEnabledDispose)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Methods

        public abstract void Clear();

        #endregion

    }

}