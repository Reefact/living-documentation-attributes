#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.EnterpriseIntegration {

    /// <summary>
    ///     ProcessManager (Enterprise Integration Patterns) — Keeps the state of a multi-step process in one
    ///     participant, so that a sequence with branches and joins can be decided as it goes rather than fixed when it
    ///     starts.
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
    public static class ProcessManager {

        /// <summary>
        ///     Role played by a type or a member in the ProcessManager design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The central participant that receives each reply and decides the next step. It is the alternative to a
        ///     routing slip, and the trade is stated: it can branch on what the replies say, at the price of being a
        ///     participant that holds state and can become a bottleneck.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ProcessManagerAttribute : Role {

            /// <summary>
            ///     The <see cref="ProcessInstanceAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ProcessInstance { get; init; }

        }

        /// <summary>
        ///     One running occurrence of the process, holding where it has got to. Separate from the manager because a
        ///     manager serves many at once, and conflating them is how a process manager becomes a single-threaded one.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ProcessInstanceAttribute : Role { }

        /// <summary>
        ///     The definition the instances follow. It exists so that changing how a process runs is configuration
        ///     rather than a class, which is the same knowledge-level move a posting rule makes for money.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ProcessTemplateAttribute : Role { }

    }

}
