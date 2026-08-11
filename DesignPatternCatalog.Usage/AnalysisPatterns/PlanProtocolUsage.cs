#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.PlanProtocolSample {

    // Every intermediate docking follows the same procedure, and the yard has run it four hundred times. Held
    // as a plan copied and edited each time, the procedure exists in four hundred slightly different versions
    // and the improvement someone found last year reached none of them.
    //
    // PLAN PROTOCOL puts the procedure at the knowledge level. A plan becomes an instance of it, and changing
    // how the work is meant to be done is configuration rather than four hundred edits.
    //
    // Note this is NOT the protocol of chapter 3, which records the method by which an observation was made.
    // The book spells both "Protocol"; this catalogue cannot, so this one carries the name of what it is the
    // knowledge level OF.

    /// <summary>One step of a procedure, and the procedure that step refers to.</summary>
    /// <remarks>
    ///     The indirection is what lets procedures compose — a step is itself a whole procedure — without one
    ///     containing a copy of another.
    /// </remarks>
    [PlanProtocol.ProtocolReference(Protocol = typeof(DockingProtocol))]
    public sealed class ProtocolStep {

        public ProtocolStep(DockingProtocol owner, DockingProtocol referred) {
            Owner    = owner;
            Referred = referred;
        }

        public DockingProtocol Owner { get; }

        /// <summary>What this step is, as a procedure in its own right.</summary>
        public DockingProtocol Referred { get; }

    }

    /// <summary>That one step must follow another.</summary>
    /// <remarks>
    ///     Stated once, for every plan the procedure yields. That is the difference from a plan's own
    ///     dependencies, which are stated per plan.
    /// </remarks>
    [PlanProtocol.ProtocolDependency(ProtocolReference = typeof(ProtocolStep))]
    public sealed class StepOrder {

        public StepOrder(ProtocolStep dependent, ProtocolStep consequent) {
            if (dependent.Owner != consequent.Owner) {
                throw new ArgumentException("both steps belong to one procedure", nameof(consequent));
            }
            Dependent  = dependent;
            Consequent = consequent;
        }

        public ProtocolStep Dependent { get; }

        public ProtocolStep Consequent { get; }

    }

    /// <summary>The standard procedure, as a type object.</summary>
    /// <remarks>
    ///     The knowledge level of a plan: an ordered set of steps that plans are drawn from.
    /// </remarks>
    [PlanProtocol.Protocol]
    public sealed class DockingProtocol {

        private readonly List<ProtocolStep> _steps = new List<ProtocolStep>();

        public DockingProtocol(string name) { Name = name; }

        /// <summary>"Intermediate docking", "Hull blasting".</summary>
        public string Name { get; }

        /// <summary>
        ///     The steps making up the procedure, in order.
        /// </summary>
        /// <remarks>
        ///     What the procedure reaches is derived from its steps' referred procedures rather than listed a
        ///     second time.
        /// </remarks>
        [PlanProtocol.Steps]
        public IReadOnlyList<ProtocolStep> Steps => _steps;

        public ProtocolStep AddStep(DockingProtocol referred) {
            ProtocolStep step = new(this, referred);
            _steps.Add(step);

            return step;
        }

    }

}
