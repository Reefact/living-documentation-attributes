#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.StatusTypeSample {

    // Budget season at the same co-operative. Every figure the board sees exists three times over: what was
    // budgeted, what happened, and the gap. The obvious model gives the report class three columns, and then a
    // fourth when a reforecast lands in February, and a fifth for last year's actual.
    //
    // STATUS TYPE makes the status a type object the figure names. A budget is then a set of figures rather than
    // a column, a reforecast is another set, and a variance is itself a figure — so it can be planned too, which
    // is what a target for an improvement is.

    /// <summary>
    ///     What a figure is claiming about itself.
    /// </summary>
    /// <remarks>
    ///     A type object, so which statuses exist is configuration. A subclass per status, or a boolean, makes
    ///     comparing a plan against an outcome a special case instead of an ordinary figure.
    /// </remarks>
    [StatusType.StatusType]
    public abstract class FigureStatus {

        protected FigureStatus(string name) {
            Name = name;
        }

        public string Name { get; }

    }

    /// <summary>
    ///     Of the world as it was.
    /// </summary>
    /// <remarks>
    ///     The offset distinguishes a figure stated a month after its period from the same figure restated at
    ///     year end — the flash result and the audited one are both actual.
    /// </remarks>
    [StatusType.Actual]
    public sealed class Actual : FigureStatus {

        public Actual(TimeSpan? statedAfter = null)
            : base("actual") {
            StatedAfter = statedAfter;
        }

        public TimeSpan? StatedAfter { get; }

    }

    /// <summary>
    ///     Belonging to a plan, and naming which.
    /// </summary>
    /// <remarks>
    ///     Two plans for one period are two sets of figures rather than two columns on one.
    /// </remarks>
    [StatusType.Planned(Plan = typeof(Budget))]
    public sealed class Planned : FigureStatus {

        public Planned(Budget plan)
            : base("planned") {
            Plan = plan;
        }

        public Budget Plan { get; }

    }

    /// <summary>
    ///     The plan a planned figure belongs to.
    /// </summary>
    /// <remarks>
    ///     An object rather than a label, so one plan's figures can be gathered and a plan superseded without
    ///     rewriting what it held.
    /// </remarks>
    [StatusType.Plan]
    public sealed class Budget {

        public Budget(string name, int season, Budget? supersedes) {
            Name       = name;
            Season     = season;
            Supersedes = supersedes;
        }

        /// <summary>"Board budget", "February reforecast".</summary>
        public string Name { get; }

        public int Season { get; }

        /// <summary>Null for the first. A reforecast does not erase what it replaces.</summary>
        public Budget? Supersedes { get; }

    }

    /// <summary>
    ///     A figure that compares two others.
    /// </summary>
    /// <remarks>
    ///     A status and not a calculation, because what it yields is a figure like any other — and so may itself
    ///     be planned, which is what a variance target is.
    /// </remarks>
    [StatusType.ComparativeStatusType]
    public sealed class Variance : FigureStatus {

        public Variance(FigureStatus datum, FigureStatus comparator)
            : base("variance") {
            Datum      = datum;
            Comparator = comparator;
        }

        /// <summary>The figure being judged — usually the actual.</summary>
        public FigureStatus Datum { get; }

        /// <summary>What it is judged against.</summary>
        public FigureStatus Comparator { get; }

    }

    /// <summary>
    ///     One figure on one line of the management accounts.
    /// </summary>
    public sealed class Figure {

        public Figure(string line, decimal amount, FigureStatus status) {
            Line   = line;
            Amount = amount;
            Status = status;
        }

        public string Line { get; }

        public decimal Amount { get; }

        /// <summary>
        ///     What this figure claims about itself.
        /// </summary>
        /// <remarks>
        ///     One reference, on the figure rather than on a subtype of it, is what makes planned and actual
        ///     interchangeable to every report that reads figures.
        /// </remarks>
        [StatusType.Status]
        public FigureStatus Status { get; }

    }

}
