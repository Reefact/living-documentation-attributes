#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.Idioms {

    /// <summary>
    ///     NullObject (no catalog of its own) — A special case whose behaviour is to do nothing, so that the absence of
    ///     a collaborator needs no null check.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Bobby Woolf, <i>Pattern Languages of Program Design 3</i>, 1997.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public sealed class NullObjectAttribute : DesignPatternAttribute { }

}
