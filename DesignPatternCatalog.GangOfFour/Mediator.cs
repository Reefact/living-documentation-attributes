#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.GangOfFour {

    /// <summary>
    ///     Mediator (Gang of Four) — Defines an object that encapsulates how a set of objects interact, keeping them
    ///     from referring to each other explicitly.
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
    public static class Mediator {

        /// <summary>
        ///     Role played by a type or a member in the Mediator design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Declares the interface through which colleagues communicate.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class MediatorAttribute : Role { }

        /// <summary>
        ///     Knows the colleagues and coordinates their interactions.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteMediatorAttribute : Role {

            /// <summary>
            ///     The <see cref="MediatorAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Mediator { get; init; }

        }

        /// <summary>
        ///     Communicates with the other participants only through the mediator.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ColleagueAttribute : Role {

            /// <summary>
            ///     The <see cref="MediatorAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Mediator { get; init; }

        }

        /// <summary>
        ///     One participant of the interaction.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteColleagueAttribute : Role {

            /// <summary>
            ///     The <see cref="ColleagueAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Colleague { get; init; }

        }

    }

}
