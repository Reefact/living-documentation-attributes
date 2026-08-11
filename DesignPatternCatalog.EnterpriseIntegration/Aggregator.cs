#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     Aggregator (Enterprise Integration Patterns) — Collects related messages and emits one message when the set
    ///     is complete, so that a result assembled from many parts can be treated as a whole.
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
    public static class Aggregator {

        /// <summary>
        ///     Role played by a type or a member in the Aggregator design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The stateful participant that holds messages until they belong together. It is the counterpart of a
        ///     splitter, and being stateful is what distinguishes it from every other router — it must survive a
        ///     restart or lose a half-finished set.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class AggregatorAttribute : Role { }

        /// <summary>
        ///     What decides that two messages belong to the same set. Named explicitly because getting it wrong merges
        ///     two unrelated consignments, and nothing else in the pattern would notice.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class CorrelationAttribute : Role { }

        /// <summary>
        ///     What decides that a set is finished — a count, a timeout, an end marker. The hard part of the pattern,
        ///     since a condition that never holds is a set that never emits and a leak nobody sees.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class CompletenessConditionAttribute : Role { }

        /// <summary>
        ///     How the collected messages become one. Held apart from the completeness condition because when to emit
        ///     and what to emit are different questions, and conflating them is how an aggregator becomes unreadable.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class AggregationStrategyAttribute : Role { }

    }

}
