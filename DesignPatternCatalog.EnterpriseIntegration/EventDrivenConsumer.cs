#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     EventDrivenConsumer (Enterprise Integration Patterns) — Is handed each message as it arrives, so that an
    ///     application consumes as fast as the channel delivers without a thread of its own waiting.
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
    public sealed class EventDrivenConsumerAttribute : LivingDocumentationAttribute { }

}
