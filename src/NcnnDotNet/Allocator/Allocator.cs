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

        #region Methods

        internal static Allocator GetAllocator(IntPtr allocator, NativeMethods.AllocatorType type, bool isEnabledDispose = true)
        {
            switch (type)
            {
                case NativeMethods.AllocatorType.UnlockedPoolAllocator:
                    return new UnlockedPoolAllocator(allocator, isEnabledDispose);
                case NativeMethods.AllocatorType.PoolAllocator:
                    return new PoolAllocator(allocator, isEnabledDispose);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        #endregion

    }

}