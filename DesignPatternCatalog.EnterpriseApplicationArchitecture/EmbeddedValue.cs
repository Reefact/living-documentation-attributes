#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseApplicationArchitecture {

    /// <summary>
    ///     EmbeddedValue (Patterns of Enterprise Application Architecture) — A value object mapped into the columns of
    ///     the object that owns it, rather than into a table of its own.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Patterns of Enterprise Application Architecture</i>, 2002.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public sealed class EmbeddedValueAttribute : LivingDocumentationAttribute { }

}
