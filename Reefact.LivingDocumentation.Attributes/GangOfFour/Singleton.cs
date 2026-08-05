#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Singleton (Gang of Four) — Ensures a type has only one instance, and provides a global point of access to
    ///     it.
    /// </summary>
    /// <remarks>
    ///     This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class SingletonAttribute : LivingDocumentationAttribute { }

}
