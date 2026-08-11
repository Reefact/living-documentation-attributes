#region Usings declarations

using System;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.QuantitySample {

    // A brewery. Every number on the brew sheet means nothing without its unit: 55 hectolitres of wort, 12.4
    // degrees Plato of extract, 38 IBU of bitterness, 18 degrees Celsius of fermentation temperature.
    //
    // Figures 3.1 and 3.2 are the argument, and they are about the same class twice. In 3.1 the properties are
    // Numbers; in 3.2 they are Quantities. The first compiles a brewhouse in which a bitterness can be added to
    // a temperature, and the addition is silent.
    //
    // QUANTITY is the amount and its unit as one value, with arithmetic of its own — and the arithmetic's job is
    // as much to refuse as to compute. Scaling a volume by a number is meaningful; adding a volume to a mass is
    // not; comparing degrees Plato to degrees Celsius is not, even though both are "degrees" and both are
    // doubles.
    //
    // The two members are marked because they are where the pattern is lost. Anything that reads the amount
    // alone has stepped outside the guarantee, and every unit error on record begins exactly there.

    /// <summary>
    ///     An amount together with the unit that gives it meaning.
    /// </summary>
    [Quantity.Quantity]
    public readonly struct Measure : IEquatable<Measure> {

        public Measure(decimal amount, string unit) {
            Amount = amount;
            Unit   = unit;
        }

        /// <summary>The number, meaningless on its own.</summary>
        [Quantity.Amount]
        public decimal Amount { get; }

        /// <summary>What the number is measured in.</summary>
        [Quantity.Unit]
        public string Unit { get; }

        /// <summary>Adds two measures of the same unit.</summary>
        /// <exception cref="InvalidOperationException">If the units differ.</exception>
        public static Measure operator +(Measure left, Measure right) {
            Compatible(left, right, "add");

            return new Measure(left.Amount + right.Amount, left.Unit);
        }

        /// <summary>Subtracts two measures of the same unit.</summary>
        /// <exception cref="InvalidOperationException">If the units differ.</exception>
        public static Measure operator -(Measure left, Measure right) {
            Compatible(left, right, "subtract");

            return new Measure(left.Amount - right.Amount, left.Unit);
        }

        /// <summary>Scales a measure by a bare number, which is the one arithmetic that stays meaningful.</summary>
        public static Measure operator *(Measure measure, decimal factor) {
            return new Measure(measure.Amount * factor, measure.Unit);
        }

        /// <summary>Compares two measures of the same unit.</summary>
        /// <exception cref="InvalidOperationException">If the units differ — degrees Plato are not degrees Celsius.</exception>
        public static bool operator >(Measure left, Measure right) {
            Compatible(left, right, "compare");

            return left.Amount > right.Amount;
        }

        /// <summary>Compares two measures of the same unit.</summary>
        public static bool operator <(Measure left, Measure right) {
            Compatible(left, right, "compare");

            return left.Amount < right.Amount;
        }

        /// <inheritdoc />
        public bool Equals(Measure other) {
            return Amount == other.Amount && Unit == other.Unit;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj) {
            return obj is Measure other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return HashCode.Combine(Amount, Unit);
        }

        /// <inheritdoc />
        public override string ToString() {
            return $"{Amount} {Unit}";
        }

        private static void Compatible(Measure left, Measure right, string operation) {
            if (left.Unit != right.Unit) {
                throw new InvalidOperationException($"cannot {operation} {left.Unit} and {right.Unit}");
            }
        }

    }

}
