#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.MicroservicesPatterns {

    /// <summary>
    ///     Cqrs (Microservices Patterns) — Command Query Responsibility Segregation: keeps the model that changes data
    ///     apart from the models that answer questions about it, each query served by a view kept current from the
    ///     events the command side publishes.
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
    public static class Cqrs {

        /// <summary>
        ///     Role played by a type or a member in the Cqrs design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The model that changes the data and owns it. It publishes an event for every change, and it answers no
        ///     query it does not need for its own invariants.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
        public sealed class CommandSideAttribute : Role { }

        /// <summary>
        ///     The model that answers queries by reading a view. It writes nothing, which is the whole of the
        ///     segregation and the one thing a reviewer has to check.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
        public sealed class QuerySideAttribute : Role {

            /// <summary>
            ///     The <see cref="ViewAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? View { get; init; }

        }

        /// <summary>
        ///     A read-only replica shaped for one query or one family of them, in whatever store suits it. It is behind
        ///     the command side by however long the events take, and everything reading it has to be able to say so.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ViewAttribute : Role { }

        /// <summary>
        ///     Subscribes to the events the command side publishes and brings the view up to date. It is the view's
        ///     only writer, and a second one is the defect this role exists to make visible.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
        public sealed class ViewUpdaterAttribute : Role {

            /// <summary>
            ///     The <see cref="ViewAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? View { get; init; }

        }

    }

}
