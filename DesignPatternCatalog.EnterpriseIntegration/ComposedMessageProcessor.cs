#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     ComposedMessageProcessor (Enterprise Integration Patterns) — Splits a message, routes each element to the
    ///     processing it needs, and reassembles the results, so that a message of mixed elements is handled without a
    ///     step that understands all of them.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Gregor Hohpe, Bobby Woolf, <i>Enterprise Integration Patterns</i>, 2003.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class ComposedMessageProcessorAttribute : DesignPatternAttribute { }

}
