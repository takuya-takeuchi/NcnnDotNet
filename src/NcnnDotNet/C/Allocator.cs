using System;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.C
{

    public sealed class Allocator : NcnnObject
    {

        #region Constructors

        internal Allocator(IntPtr ptr):
            base(ptr)
        {
        }

        #endregion

    }

}