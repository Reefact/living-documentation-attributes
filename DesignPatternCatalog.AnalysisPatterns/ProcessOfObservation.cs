#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AnalysisPatterns {

    /// <summary>
    ///     ProcessOfObservation (Analysis Patterns) — Lets one observation stand as evidence for another, so that a
    ///     conclusion and the findings it rests on are one connected record rather than two lists.
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
    public static class ProcessOfObservation {

        /// <summary>
        ///     Role played by a type or a member in the ProcessOfObservation design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     An observation concluded from others — a diagnosis drawn from symptoms and results. Figure 3.8 draws it
        ///     as an association from observation to observation, which is the economical part: an assessment is an
        ///     observation like any other, so it can itself be evidence for a further one, and a chain of reasoning
        ///     needs no new class.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class AssessmentAttribute : Role { }

        /// <summary>
        ///     The observations the assessment rests on. Marking it makes the question "why does the record say this"
        ///     answerable from the model, and it is the member that decides what happens when one of them is rejected —
        ///     an assessment whose evidence has been withdrawn is not automatically wrong, but it can no longer be left
        ///     unexamined.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class EvidenceAttribute : Role { }

    }

}
