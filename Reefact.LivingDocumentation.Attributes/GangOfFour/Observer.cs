#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Observer (Gang of Four) — Defines a one to many dependency between objects, so that when one object changes
    ///     state all its dependents are notified and updated automatically.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides, <i>Design Patterns</i>, 1994.
    ///     </para>
    /// </remarks>
    public static class Observer {

        /// <summary>
        ///     Role played by a type or a member in the Observer design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Knows its observers, and declares the operations to attach and detach them.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class SubjectAttribute : Role {

            /// <summary>
            ///     The <see cref="ObserverAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Observer { get; init; }

        }

        /// <summary>
        ///     Holds the state of interest, and notifies its observers when it changes.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteSubjectAttribute : Role {

            /// <summary>
            ///     The <see cref="SubjectAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Subject { get; init; }

        }

        /// <summary>
        ///     Declares the update operation invoked when the observed subject changes.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ObserverAttribute : Role { }

        /// <summary>
        ///     Reacts to the notification, and keeps itself consistent with the subject.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteObserverAttribute : Role {

            /// <summary>
            ///     The <see cref="ObserverAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Observer { get; init; }

            /// <summary>
            ///     The <see cref="ConcreteSubjectAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ConcreteSubject { get; init; }

        }

        /// <summary>
        ///     The operation that informs every registered observer of a change.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class NotifyMethodAttribute : Role { }

        /// <summary>
        ///     The operation invoked on an observer when the subject has changed.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class UpdateMethodAttribute : Role { }

    }

}
