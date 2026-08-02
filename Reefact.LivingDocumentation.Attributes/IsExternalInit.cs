#if NETSTANDARD2_0 || NETCOREAPP3_1

// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices {

    /// <summary>
    ///     Lets the compiler emit <c>init</c> accessors on target frameworks whose base class library does not declare
    ///     this marker. Ships as an internal detail: it is never part of the public surface.
    /// </summary>
    internal static class IsExternalInit { }

}

#endif
