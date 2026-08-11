#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AnalysisPatterns {

    /// <summary>
    ///     OutcomeAndStartFunctions (Analysis Patterns) — Puts at the knowledge level what an action is expected to
    ///     produce and what sets a plan going, so that neither is a rule buried in the code that happens to run.
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
    public static class OutcomeAndStartFunctions {

        /// <summary>
        ///     Role played by a type or a member in the OutcomeAndStartFunctions design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     A function stated at the knowledge level, taking observation concepts as arguments. It exists as an
        ///     object so that what the business expects can be configured and inspected rather than compiled in.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class KnowledgeFunctionAttribute : Role { }

        /// <summary>
        ///     What an action is expected to bring about, as a target and as side effects. Stating it makes the outcome
        ///     of an action checkable against what was intended, which is what turns a completed action into evidence.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class OutcomeFunctionAttribute : Role { }

        /// <summary>
        ///     What observation sets a plan going, and which protocol it indicates. It is the reason a plan can begin
        ///     because of something observed rather than because somebody noticed.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class StartFunctionAttribute : Role {

            /// <summary>
            ///     The <see cref="OutcomeAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Outcome { get; init; }

        }

        /// <summary>
        ///     The observation an action produced, tying the plan back to the world. It is an observation like any
        ///     other, which is what lets an outcome be the trigger of the next plan.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class OutcomeAttribute : Role { }

        /// <summary>
        ///     The observation concepts a knowledge function ranges over. Named on the function rather than assumed by
        ///     its caller, so a function says for itself what it needs.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class ArgumentsAttribute : Role { }

    }

}
