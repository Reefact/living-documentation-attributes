#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AnalysisPatterns {

    /// <summary>
    ///     SubtypingObservationConcepts (Analysis Patterns) — Lets the concepts an observation is stated in terms of
    ///     generalise one another as a directed acyclic graph, so that a query about a broad concept reaches every
    ///     narrower one.
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
    public static class SubtypingObservationConcepts {

        /// <summary>
        ///     Role played by a type or a member in the SubtypingObservationConcepts design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     What an observation is stated in terms of — a phenomenon type or a phenomenon indifferently, which is
        ///     why figure 3.10 puts both under it. Generalising here rather than in the class hierarchy is what lets a
        ///     clinical vocabulary be loaded rather than compiled, and those vocabularies have tens of thousands of
        ///     terms.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ObservationConceptAttribute : Role { }

        /// <summary>
        ///     The broader concepts this one falls under, plural. Figure 3.10 marks it {dag}, and that is the assertion
        ///     worth checking, because it differs from the party-type case in both directions: a concept may have
        ///     several supertypes — a viral pneumonia is both an infection and a lung disease — and no cycle is
        ///     permitted. A traversal written for a tree terminates here by luck and reports one of the two parents.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class SupertypesAttribute : Role { }

    }

}
