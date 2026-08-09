#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.AnalysisPatterns {

    /// <summary>
    ///     PlanProtocol (Analysis Patterns) — Puts the standard procedure at the knowledge level, so that a plan is an
    ///     instance of something configured rather than a shape someone remembers to repeat.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Analysis Patterns</i>, 1997.
    ///     </para>
    /// </remarks>
    public static class PlanProtocol {

        /// <summary>
        ///     Role played by a type or a member in the PlanProtocol design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The procedure as a type object: an ordered set of steps that plans are drawn from. It is the knowledge
        ///     level of a plan, so changing how the work is meant to be done is configuration and not a new plan per
        ///     case.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ProtocolAttribute : Role { }

        /// <summary>
        ///     One step of a protocol, and the protocol that step refers to. That indirection is what lets protocols
        ///     compose — a step is itself a whole procedure — without a protocol containing copies of another.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ProtocolReferenceAttribute : Role {

            /// <summary>
            ///     The <see cref="ProtocolAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Protocol { get; init; }

        }

        /// <summary>
        ///     That one step must follow another. Both are steps of the same protocol and the graph is acyclic, exactly
        ///     as in a plan; the difference is that this is stated once for every plan the protocol yields.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ProtocolDependencyAttribute : Role {

            /// <summary>
            ///     The <see cref="ProtocolReferenceAttribute" /> this role is bound to. Optional: it is only needed
            ///     when the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ProtocolReference { get; init; }

        }

        /// <summary>
        ///     The references making up the protocol, in order. The components a protocol reaches are derived from its
        ///     steps' referred protocols rather than listed a second time.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class StepsAttribute : Role { }

    }

}
