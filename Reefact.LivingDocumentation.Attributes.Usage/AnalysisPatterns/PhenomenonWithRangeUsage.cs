#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.AnalysisPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AnalysisPatterns.PhenomenonWithRangeSample {

    // Milk is graded on somatic cell count: Premium, Standard, Demoted. The grade drives the payout, appears on
    // the supplier's statement, and is argued about — and the thresholds move when the regulator moves them.
    //
    // Written as comparisons, the thresholds live in whatever code needed them, and the pricing engine and the
    // statement disagree for a fortnight after a change. PHENOMENON WITH RANGE makes the grade carry its own
    // range, and the classification a function over those ranges, so a threshold is data and there is one
    // answer to what grade a count is.

    /// <summary>
    ///     A grade, and the counts it stands for.
    /// </summary>
    /// <remarks>
    ///     The grades of one measurement should neither overlap nor leave a gap; together those two make the
    ///     classification total, which is why a count can always be graded.
    /// </remarks>
    [PhenomenonWithRange.RangedPhenomenon]
    public sealed class Grade {

        public Grade(string name, int? from, int? to) {
            Name = name;
            From = from;
            To   = to;
        }

        /// <summary>Premium, Standard, Demoted.</summary>
        public string Name { get; }

        /// <summary>
        ///     The counts this grade covers — inclusive below, exclusive above, null for unbounded.
        /// </summary>
        /// <remarks>
        ///     On the grade rather than in the function, so a regulator's change is data and the function is
        ///     untouched.
        /// </remarks>
        [PhenomenonWithRange.Range]
        public (int? From, int? To) Bounds => (From, To);

        public int? From { get; }

        public int? To { get; }

        public bool Includes(int count) {
            return (!From.HasValue || count >= From.Value)
                && (!To.HasValue || count < To.Value);
        }

    }

    /// <summary>
    ///     What grade a count is.
    /// </summary>
    /// <remarks>
    ///     One definition of the classification, rather than the same comparisons repeated wherever a grade was
    ///     needed. What it yields is derived from the count, so the count is worth keeping alongside it.
    /// </remarks>
    [PhenomenonWithRange.RangeFunction]
    public interface IGrading {

        Grade Of(int somaticCellCount);

    }

    /// <summary>
    ///     The grading in force for one season.
    /// </summary>
    public sealed class SeasonGrading : IGrading {

        private readonly IReadOnlyList<Grade> _grades;

        public SeasonGrading(IReadOnlyList<Grade> grades) {
            _grades = grades;
        }

        public Grade Of(int somaticCellCount) {
            foreach (Grade grade in _grades) {
                if (grade.Includes(somaticCellCount)) { return grade; }
            }
            throw new InvalidOperationException($"the grades leave {somaticCellCount} uncovered");
        }

    }

}
