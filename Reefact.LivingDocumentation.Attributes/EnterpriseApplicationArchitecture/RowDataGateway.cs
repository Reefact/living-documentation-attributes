#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture {

    /// <summary>
    ///     RowDataGateway (Patterns of Enterprise Application Architecture) — An object that looks exactly like one
    ///     record, holding its data and the statements that read and write it.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         A narrower case of Gateway, in Patterns of Enterprise Application Architecture: every participant
    ///         annotated here is one of those too, and a consumer asking for the broader pattern gets these as well.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Patterns of Enterprise Application Architecture</i>, 2002.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class RowDataGatewayAttribute : EnterpriseApplicationArchitecture.GatewayAttribute { }

}
