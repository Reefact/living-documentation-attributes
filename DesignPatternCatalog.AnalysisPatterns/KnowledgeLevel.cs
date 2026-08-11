#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AnalysisPatterns {

    /// <summary>
    ///     KnowledgeLevel (Analysis Patterns) — Splits a model into the objects that record what happened and the
    ///     objects that say what is allowed to happen, so that the rules change by configuring data rather than by
    ///     writing code.
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
    public static class KnowledgeLevel {

        /// <summary>
        ///     Role played by a type or a member in the KnowledgeLevel design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     What the business allows. It is populated by configuration rather than by deployment, which is the whole
        ///     return on the pattern — and it must never refer downwards, because an allowed shape that mentions one of
        ///     the things it permits has stopped being a rule and become an instance.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class KnowledgeAttribute : Role { }

        /// <summary>
        ///     What actually happened, and the level a user works in all day. Each operational object names its
        ///     knowledge-level counterpart, and the direction of that reference is the assertion a rule can check: it
        ///     runs one way only, and a reference the other way is how the two levels quietly become one.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class OperationalAttribute : Role {

            /// <summary>
            ///     The <see cref="KnowledgeAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Knowledge { get; init; }

        }

    }

}
