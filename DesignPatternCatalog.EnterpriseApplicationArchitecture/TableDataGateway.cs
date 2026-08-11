#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseApplicationArchitecture {

    /// <summary>
    ///     TableDataGateway (Patterns of Enterprise Application Architecture) — One object that carries all the SQL for
    ///     one table, and hands back results without holding any state about them.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         A narrower case of Gateway: every participant annotated here is one of those too, and a consumer asking
    ///         for the broader pattern gets these as well.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Patterns of Enterprise Application Architecture</i>, 2002.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class TableDataGatewayAttribute : GatewayAttribute { }

}
