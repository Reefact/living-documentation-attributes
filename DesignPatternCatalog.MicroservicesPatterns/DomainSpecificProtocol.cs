#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.MicroservicesPatterns {

    /// <summary>
    ///     DomainSpecificProtocol (Microservices Patterns) — Has services collaborate over a protocol belonging to
    ///     their domain — mail, media streaming — rather than over a general-purpose request/reply or messaging one.
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
    public sealed class DomainSpecificProtocolAttribute : LivingDocumentationAttribute { }

}
