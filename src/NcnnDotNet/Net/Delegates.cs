using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Layer LayerCreatorFunc();

}