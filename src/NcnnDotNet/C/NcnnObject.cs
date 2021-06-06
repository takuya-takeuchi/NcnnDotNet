using System;

namespace NcnnDotNet.C
{

    /// <summary>
    /// A class which has a pointer of native structure.
    /// </summary>
    public abstract class NcnnObject
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NcnnObject"/> class with the specified value indicating whether this instance is disposable.
        /// </summary>
        protected NcnnObject(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));
            
            this._NativePtr = ptr;
        }

        #endregion

        #region Properties

        private readonly IntPtr _NativePtr;
        
        /// <summary>
        /// Gets a pointer of native structure.
        /// </summary>>
        internal IntPtr NativePtr => this._NativePtr;

        #endregion

        #region Methods

        #region Overrides

        protected bool Equals(NcnnObject other)
        {
            return this.NativePtr.Equals(other.NativePtr);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((NcnnObject) obj);
        }

        public override int GetHashCode()
        {
            return this.NativePtr.GetHashCode();
        }

        #endregion

        #endregion

    }

}
