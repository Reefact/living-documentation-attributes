#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.MicroservicesPatterns {

    /// <summary>
    ///     DecomposeBySubdomain (Microservices Patterns) — Draws a service around one subdomain of the business as
    ///     domain-driven design identifies them, so that the boundary follows the model rather than what the
    ///     organisation happens to do.
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
    public sealed class DecomposeBySubdomainAttribute : DesignPatternAttribute { }

}
