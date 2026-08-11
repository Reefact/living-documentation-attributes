#region Usings declarations

using System;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.AccountabilitySample {

    // A multi-academy trust: thirty-one schools, one board, and an inspectorate that asks questions of the
    // form "who was answerable for safeguarding at this school in March?"
    //
    // That question is why the responsibility has to be an object. The model that answers it badly puts a
    // `SafeguardingLead` reference on the school, and it fails in three ways at once: it cannot say since
    // when, it cannot say that the previous lead was the one in post in March, and when the trust decides
    // that finance needs the same treatment, it grows a second reference beside the first — and then a third.
    //
    // ACCOUNTABILITY makes the responsibility itself the object: one party answerable to another, of a stated
    // kind, for a period. New kinds of answerability then stop being code. "Careers lead" arrives as a row.
    //
    // Which end is which is the assertion nobody else makes. Both ends are parties, so nothing in the type
    // system distinguishes them — a model that has them the wrong way round compiles, passes its tests, and
    // reports that the trust board answers to each of its schools. That is why the two members are annotated
    // rather than merely named.
    //
    // The type object carries the constraint as well as the label, and that is what earns the indirection: a
    // safeguarding lead must be a person and the trust board must not be, and the sentence saying so lives
    // once here instead of in every screen that creates one.
    //
    // Notice what is *not* modelled: no `Person` field anywhere. The responsible party of "budget holder" is
    // a school, not whoever runs it. Fowler's POST is the other half of that story.

    /// <summary>
    ///     What kind of answerability this is, and who may stand at each end of it.
    /// </summary>
    /// <remarks>
    ///     The knowledge-level object of this pattern. A new kind of responsibility is configured here, which
    ///     is the return on reifying the relationship at all.
    /// </remarks>
    [Accountability.AccountabilityType]
    public sealed class ResponsibilityKind {

        public ResponsibilityKind(string name, bool responsibleMustBeAPerson) {
            Name                     = name;
            ResponsibleMustBeAPerson = responsibleMustBeAPerson;
        }

        /// <summary>What the trust calls it: line management, budget holder, safeguarding lead.</summary>
        public string Name { get; }

        /// <summary>
        ///     Whether this kind may only be held by a person. Safeguarding may not be held by a school; a
        ///     budget may only be held by one.
        /// </summary>
        public bool ResponsibleMustBeAPerson { get; }

    }

    /// <summary>
    ///     One responsibility, of one kind, held by one party towards another, over a period.
    /// </summary>
    /// <remarks>
    ///     The dates are the point: an inspector asks who was answerable in March, and a model that overwrote
    ///     a reference cannot answer.
    /// </remarks>
    [Accountability.Accountability(AccountabilityType = typeof(ResponsibilityKind))]
    public sealed class Responsibility {

        public Responsibility(ResponsibilityKind kind, Party commissioner, Party responsible,
                              DateOnly from, DateOnly? until) {
            Kind          = kind;
            Commissioner  = commissioner;
            Responsible   = responsible;
            From          = from;
            Until         = until;
        }

        /// <summary>What kind of answerability this is.</summary>
        public ResponsibilityKind Kind { get; }

        /// <summary>
        ///     The party the responsibility is owed to — the one who may ask for an account of it.
        /// </summary>
        [Accountability.Commissioner]
        public Party Commissioner { get; }

        /// <summary>
        ///     The party that holds it. A school, a trust, or a person: the pattern's reach depends on this
        ///     not being narrowed to people.
        /// </summary>
        [Accountability.Responsible]
        public Party Responsible { get; }

        /// <summary>When it started.</summary>
        public DateOnly From { get; }

        /// <summary>When it ended, if it has.</summary>
        public DateOnly? Until { get; }

        /// <summary>Whether this responsibility was in force on a given day.</summary>
        public bool InForceOn(DateOnly day) {
            return day >= From && (Until is null || day <= Until);
        }

    }

    /// <summary>
    ///     A school, the trust, or a member of staff — anything that can be made answerable.
    /// </summary>
    /// <remarks>
    ///     Fowler's PARTY, which ACCOUNTABILITY is stated in terms of. Both ends are one of these, which is
    ///     precisely why the two ends have to be annotated.
    /// </remarks>
    [Party]
    public abstract class Party {

        protected Party(string name) {
            Name = name;
        }

        /// <summary>What it is called.</summary>
        public string Name { get; }

    }

    /// <summary>One of the thirty-one schools.</summary>
    public sealed class School : Party {

        public School(string name, string urn) : base(name) {
            Urn = urn;
        }

        /// <summary>The unique reference number the department knows it by.</summary>
        public string Urn { get; }

    }

    /// <summary>A member of staff.</summary>
    public sealed class Employee : Party {

        public Employee(string name, string payrollNumber) : base(name) {
            PayrollNumber = payrollNumber;
        }

        /// <summary>The payroll number.</summary>
        public string PayrollNumber { get; }

    }

}
