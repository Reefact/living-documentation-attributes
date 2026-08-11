#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     PipesAndFilters (Enterprise Integration Patterns) — Divides a larger processing task into a sequence of
    ///     independent steps joined by channels, so that a step can be reordered, reused or replaced without the others
    ///     knowing.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Gregor Hohpe, Bobby Woolf, <i>Enterprise Integration Patterns</i>, 2003.
    ///     </para>
    /// </remarks>
    public static class PipesAndFilters {

        /// <summary>
        ///     Role played by a type or a member in the PipesAndFilters design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     One processing step. It reads from a channel and writes to a channel, and knows nothing of what precedes
        ///     or follows it — which is the whole property that lets the sequence be rearranged.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class FilterAttribute : Role { }

        /// <summary>
        ///     The channel joining two steps. Making it a participant rather than a call is what decouples the steps in
        ///     time and lets one be replaced while the other runs.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class PipeAttribute : Role { }

        /// <summary>
        ///     The assembled sequence. It is the only participant that knows the order, so the order is stated in one
        ///     place rather than implied by who calls whom.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class PipelineAttribute : Role {

            /// <summary>
            ///     The <see cref="FilterAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Filter { get; init; }

        }

    }

}
