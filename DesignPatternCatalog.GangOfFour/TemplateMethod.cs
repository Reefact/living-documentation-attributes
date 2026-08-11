#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.GangOfFour {

    /// <summary>
    ///     TemplateMethod (Gang of Four) — Defines the skeleton of an algorithm in an operation, deferring some steps
    ///     to subclasses so they can redefine them without changing the algorithm's structure.
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
    public static class TemplateMethod {

        /// <summary>
        ///     Role played by a type or a member in the TemplateMethod design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Defines the skeleton of the algorithm, and declares the steps subclasses must supply.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class AbstractClassAttribute : Role { }

        /// <summary>
        ///     Supplies the steps the algorithm defers to subclasses.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteClassAttribute : Role {

            /// <summary>
            ///     The <see cref="AbstractClassAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? AbstractClass { get; init; }

        }

        /// <summary>
        ///     The operation that holds the skeleton of the algorithm, and calls the deferred steps.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class TemplateMethodAttribute : Role { }

        /// <summary>
        ///     A step the algorithm defers, and which subclasses must supply.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class PrimitiveOperationAttribute : Role { }

        /// <summary>
        ///     A step the algorithm defers, and which subclasses may override, but need not.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class HookOperationAttribute : Role { }

    }

}
