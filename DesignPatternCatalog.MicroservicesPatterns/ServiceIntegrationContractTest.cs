#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.MicroservicesPatterns {

    /// <summary>
    ///     ServiceIntegrationContractTest (Microservices Patterns) — Has the developers of a consuming service write
    ///     the suite that verifies the provider's API, so that the provider finds out it has broken somebody in its own
    ///     build rather than in production.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Chris Richardson, <i>Microservices Patterns</i>, 2018.
    ///     </para>
    /// </remarks>
    public static class ServiceIntegrationContractTest {

        /// <summary>
        ///     Role played by a type or a member in the ServiceIntegrationContractTest design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The suite, written by one team and run by another. That split is the whole pattern and the thing a test
        ///     class does not show: it fails for people who did not write it, who cannot tell from reading it whether
        ///     the expectation still holds.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class ServiceIntegrationContractTestAttribute : Role {

            /// <summary>
            ///     The <see cref="ProviderAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Provider { get; init; }

            /// <summary>
            ///     The <see cref="ConsumerAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Consumer { get; init; }

        }

        /// <summary>
        ///     The service whose API is under test. Green says it still meets the expectations of the consumers who
        ///     wrote a suite, and says nothing whatever about the consumers who did not.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
        public sealed class ProviderAttribute : Role { }

        /// <summary>
        ///     The service whose developers wrote the suite and whose expectations it encodes. The work leaves an open
        ///     question against this role: nothing checks that what they wrote down is what they actually require.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
        public sealed class ConsumerAttribute : Role { }

    }

}
