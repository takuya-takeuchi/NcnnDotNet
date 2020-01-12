using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public abstract class DataReader : NcnnObject
    {

        #region Delegates

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OnConstructorDelegate(IntPtr windows);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OnDestructorDelegate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int OnScanDelegate(IntPtr format, IntPtr p);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate long OnReadDelegate(IntPtr buf, long size);

        #endregion

        #region Fields

        private readonly DelegateHandler<OnConstructorDelegate> _Constructor;

        private readonly DelegateHandler<OnDestructorDelegate> _Destructor;

        private readonly DelegateHandler<OnScanDelegate> _OnScan;

        private readonly DelegateHandler<OnReadDelegate> _OnRead;

        #endregion

        #region Constructors

        protected DataReader()
        {
            this._Constructor = new DelegateHandler<OnConstructorDelegate>(this.Constructor);
            this._Destructor = new DelegateHandler<OnDestructorDelegate>(this.Destructor);
            this._OnScan = new DelegateHandler<OnScanDelegate>(this.Scan);
            this._OnRead = new DelegateHandler<OnReadDelegate>(this.Read);

            this.NativePtr = NativeMethods.datareader_DataReader_new(this._Constructor.Handle,
                                                                     this._Destructor.Handle,
                                                                     this._OnScan.Handle,
                                                                     this._OnRead.Handle);
        }

        #endregion

        #region Methods

        protected virtual void OnConstructor(IntPtr reader)
        {

        }

        protected virtual void OnDestructor()
        {

        }

        protected virtual int OnScan(IntPtr format, IntPtr p)
        {
            return 0;
        }

        protected virtual long OnRead(IntPtr buf, long size)
        {
            return 0;
        }

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.datareader_DataReader_delete(this.NativePtr);
        }

        #endregion

        #region Helpers

        private void Constructor(IntPtr reader)
        {
            this.OnConstructor(reader);
        }

        private void Destructor()
        {
            this.OnDestructor();
        }

        private int Scan(IntPtr format, IntPtr p)
        {
            return this.OnScan(format, p);
        }

        private long Read(IntPtr buf, long size)
        {
            return this.OnRead(buf, size);
        }

        #endregion

        #endregion

    }

}