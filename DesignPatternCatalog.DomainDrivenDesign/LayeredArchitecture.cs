#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.DomainDrivenDesign {

    /// <summary>
    ///     LayeredArchitecture (Domain-Driven Design) — Partitions a system so that the model is isolated from the user
    ///     interface, the application logic and the technical plumbing, and can be reasoned about without any of them.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Eric Evans, <i>Domain-Driven Design</i>, 2003.
    ///     </para>
    /// </remarks>
    public static class LayeredArchitecture {

        /// <summary>
        ///     Role played by a type or a member in the LayeredArchitecture design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Shows information to the user and interprets what the user does. It holds no rule of the domain:
        ///     everything it shows was decided below it, and a rule found here is a rule no other channel can reach.
        /// </summary>
        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class UserInterfaceAttribute : Role { }

        /// <summary>
        ///     Coordinates the work — it opens the transaction, calls the model and reports what happened — and is kept
        ///     deliberately thin. It states what the system does, never what the business is, which is what stops it
        ///     turning into a second model.
        /// </summary>
        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class ApplicationAttribute : Role { }

        /// <summary>
        ///     The business concepts, their state and their rules. It is the reason the other three exist, and the
        ///     whole point of naming the layers is that this one references none of them.
        /// </summary>
        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class DomainAttribute : Role { }

        /// <summary>
        ///     The technical means the layers above stand on — persistence, messaging, drawing on a screen. It
        ///     implements what they declare rather than being called into their vocabulary, which is the inversion that
        ///     keeps the model free of a database.
        /// </summary>
        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class InfrastructureAttribute : Role { }

    }

}
