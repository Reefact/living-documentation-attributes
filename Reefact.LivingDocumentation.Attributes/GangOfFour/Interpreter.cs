#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Interpreter (Gang of Four) — Given a language, defines a representation for its grammar together with an
    ///     interpreter that uses that representation to interpret sentences of the language.
    /// </summary>
    /// <remarks>
    ///     Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate that
    ///     interface rather than each of its implementations.
    /// </remarks>
    public static class Interpreter {

        /// <summary>
        ///     Role played by a type or a member in the Interpreter design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Declares the interpretation operation shared by every node of the syntax tree.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class AbstractExpressionAttribute : Role { }

        /// <summary>
        ///     Interprets a terminal symbol of the grammar: it has no sub expression.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class TerminalExpressionAttribute : Role {

            /// <summary>
            ///     The <see cref="AbstractExpressionAttribute" /> this role is bound to. Optional: it is only needed
            ///     when the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? AbstractExpression { get; init; }

        }

        /// <summary>
        ///     Interprets a grammar rule by delegating to its sub expressions.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class NonterminalExpressionAttribute : Role {

            /// <summary>
            ///     The <see cref="AbstractExpressionAttribute" /> this role is bound to. Optional: it is only needed
            ///     when the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? AbstractExpression { get; init; }

        }

        /// <summary>
        ///     Carries the information global to the interpretation.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class ContextAttribute : Role { }

    }

}
