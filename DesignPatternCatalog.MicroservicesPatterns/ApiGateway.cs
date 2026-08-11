#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.MicroservicesPatterns {

    /// <summary>
    ///     ApiGateway (Microservices Patterns) — Gives every client one entry point, routing some requests to a single
    ///     service and answering others by fanning out to several, so that no client has to know how the application is
    ///     partitioned.
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
    public class ApiGatewayAttribute : LivingDocumentationAttribute { }

}
