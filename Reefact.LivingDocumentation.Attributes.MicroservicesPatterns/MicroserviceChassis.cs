#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.MicroservicesPatterns {

    /// <summary>
    ///     MicroserviceChassis (Microservices Patterns) — Gathers the cross-cutting concerns every service needs —
    ///     configuration, logging, health checks, metrics, discovery — into a framework, so that a new service starts
    ///     from them rather than reimplementing them.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Chris Richardson, <i>Microservices Patterns</i>, 2018.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public sealed class MicroserviceChassisAttribute : LivingDocumentationAttribute { }

}
