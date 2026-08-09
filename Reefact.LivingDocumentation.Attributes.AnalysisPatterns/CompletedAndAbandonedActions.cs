#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.AnalysisPatterns {

    /// <summary>
    ///     CompletedAndAbandonedActions (Analysis Patterns) — Treats abandonment and completion as two independent
    ///     facts about an action, so that giving up before starting and giving up halfway are the same kind of record.
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
    public static class CompletedAndAbandonedActions {

        /// <summary>
        ///     Role played by a type or a member in the CompletedAndAbandonedActions design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     An action given up on, whether or not it was ever started. Abandonment cuts across the proposed and
        ///     implemented split rather than sitting under one side of it, which is what stops the model needing two
        ///     ways to say the same thing.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class AbandonedActionAttribute : Role { }

        /// <summary>
        ///     An implemented action that ran to its end. Only an implemented action can be completed, which is why
        ///     completion is not a second dimension but a narrowing of one — and why nothing needs to say that a
        ///     proposal cannot be complete.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class CompletedActionAttribute : Role { }

    }

}
