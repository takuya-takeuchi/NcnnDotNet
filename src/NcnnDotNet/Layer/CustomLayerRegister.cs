using System;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    public sealed class CustomLayerRegister : NcnnObject
    {

        #region Delegates

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr OnCreatorDelegate(IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void OnDestroyerDelegate(IntPtr layer, IntPtr userData);

        public delegate CustomLayer CreatorDelegate(IntPtr userData);

        public delegate void DestroyerDelegate(CustomLayer layer, IntPtr userData);

        #endregion

        #region Fields

        private readonly DelegateHandler<OnCreatorDelegate> _NativeCreator;

        private readonly DelegateHandler<OnDestroyerDelegate> _NativeDestroyer;

        private readonly CreatorDelegate _Creator;

        private readonly DestroyerDelegate _Destroyer;

        private readonly IntPtr _Name;

        #endregion

        #region Constructors

        public CustomLayerRegister(string name,
                                   CreatorDelegate creator,
                                   DestroyerDelegate destroyer = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"{nameof(name)}");
            if (creator == null)
                throw new ArgumentNullException($"{nameof(creator)}");

            this._NativeCreator = new DelegateHandler<OnCreatorDelegate>(this.OnCreator);
            this._NativeDestroyer = new DelegateHandler<OnDestroyerDelegate>(this.OnDestroyer);

            this._Creator = creator;
            this._Destroyer = destroyer;

            this._Name = Marshal.StringToCoTaskMemAnsi(name);
        }

        #endregion

        #region Properties

        public IntPtr Name => this._Name;

        public IntPtr Creator => this._NativeCreator.Handle;

        public IntPtr Destroyer => this._NativeDestroyer.Handle;

        #endregion

        #region Methods

        #region Helpers

        private IntPtr OnCreator(IntPtr userData)
        {
            return this._Creator.Invoke(userData).NativePtr;
        }

        private void OnDestroyer(IntPtr layer, IntPtr userData)
        {
            // ToDo: CustomLayer class is abstract but created layer object shall be disposed
            //if (this._Destroyer != null)
            //    this._Destroyer.Invoke(new CustomLayer(layer, true));
        }

        #endregion

        #region Overrides 

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            Marshal.FreeCoTaskMem(this._Name);
        }

        #endregion

        #endregion

    }

}