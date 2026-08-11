#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.MicroservicesPatterns {

    /// <summary>
    ///     CircuitBreaker (Microservices Patterns) — Invokes a remote service through a proxy that counts failures and,
    ///     past a threshold, fails immediately for a time rather than letting a slow or dead service exhaust the
    ///     caller's threads.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Chris Richardson, <i>Microservices Patterns</i>, 2018.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class CircuitBreakerAttribute : LivingDocumentationAttribute { }

}
