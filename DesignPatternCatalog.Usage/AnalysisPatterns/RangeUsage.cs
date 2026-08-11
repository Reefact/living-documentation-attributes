#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.RangeSample {

    // The co-operative's payout schedule pays a different rate per kilogram of milk solids in each volume band,
    // and the bands are renegotiated every season. Held as pairs of loose decimals, the bands get compared with
    // >= in the pricing code and > in the validation code, and one supplier a season falls in both bands or in
    // neither.
    //
    // RANGE makes the band one object that answers the comparisons itself. Whether a bound is inclusive is a
    // property of the band rather than a habit of whoever wrote the last if.

    /// <summary>
    ///     An interval of quantities, with its own comparisons.
    /// </summary>
    /// <remarks>
    ///     A value. The operations belong to it rather than to the code holding the bounds, which is what stops
    ///     one comparison being inclusive where the next is exclusive. An open end is a null bound, and a caller
    ///     never tests for it.
    /// </remarks>
    [Range]
    public readonly struct VolumeBand : IEquatable<VolumeBand> {

        public VolumeBand(decimal? lower, decimal? upper, bool isLowerInclusive = true, bool isUpperInclusive = false) {
            if (lower.HasValue && upper.HasValue && lower > upper) {
                throw new ArgumentException("a band's lower bound does not exceed its upper", nameof(lower));
            }
            Lower            = lower;
            Upper            = upper;
            IsLowerInclusive = isLowerInclusive;
            IsUpperInclusive = isUpperInclusive;
        }

        /// <summary>Null means unbounded below.</summary>
        public decimal? Lower { get; }

        /// <summary>Null means unbounded above.</summary>
        public decimal? Upper { get; }

        public bool IsLowerInclusive { get; }

        public bool IsUpperInclusive { get; }

        /// <summary>Whether a volume falls in this band.</summary>
        public bool Includes(decimal value) {
            if (Lower.HasValue && (IsLowerInclusive ? value < Lower : value <= Lower)) { return false; }
            if (Upper.HasValue && (IsUpperInclusive ? value > Upper : value >= Upper)) { return false; }
            return true;
        }

        /// <summary>Whether two bands share a volume. A schedule whose bands overlap is a defect.</summary>
        public bool Overlaps(VolumeBand other) {
            if (Upper.HasValue && other.Lower.HasValue && Upper < other.Lower) { return false; }
            if (other.Upper.HasValue && Lower.HasValue && other.Upper < Lower) { return false; }
            return true;
        }

        /// <summary>Whether two bands meet exactly. A schedule whose bands do not abut has a gap.</summary>
        public bool Abuts(VolumeBand other) {
            return (Upper.HasValue && other.Lower == Upper && IsUpperInclusive != other.IsLowerInclusive)
                || (Lower.HasValue && other.Upper == Lower && other.IsUpperInclusive != IsLowerInclusive);
        }

        public bool Equals(VolumeBand other) {
            return Lower == other.Lower
                && Upper == other.Upper
                && IsLowerInclusive == other.IsLowerInclusive
                && IsUpperInclusive == other.IsUpperInclusive;
        }

        public override bool Equals(object? obj) => obj is VolumeBand other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Lower, Upper, IsLowerInclusive, IsUpperInclusive);

    }

}
