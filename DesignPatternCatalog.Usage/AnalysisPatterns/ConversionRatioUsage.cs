#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.ConversionRatioSample {

    // A grain merchant. Contracts are written in tonnes, farmers weigh in bushels, the American buyer quotes
    // hundredweight, and a bushel of wheat is not a bushel of oats — the conversion depends on the crop.
    //
    // CONVERSION RATIO holds the factor as an object instead of as code. Figure 3.3 gives it exactly two ends
    // and a number, and the reason that beats a method is arithmetic nobody wants to write twice: with ratios as
    // data, a conversion from bushels to hundredweight can be composed through tonnes without anyone having
    // stated it, and a new crop is a row.
    //
    // The two ends are marked because both are units and nothing distinguishes them. An inverted ratio does not
    // throw: it returns a number that is wrong by a factor of thirty-six, and on a grain invoice that is a
    // plausible-looking figure.

    /// <summary>
    ///     The factor from one unit to another, held as data.
    /// </summary>
    [ConversionRatio.ConversionRatio]
    public sealed class Ratio {

        public Ratio(string from, string to, decimal number) {
            if (from == to) {
                throw new ArgumentException("a conversion ratio needs two distinct units", nameof(to));
            }

            From   = from;
            To     = to;
            Number = number;
        }

        /// <summary>The unit converted out of.</summary>
        [ConversionRatio.From]
        public string From { get; }

        /// <summary>The unit converted into.</summary>
        [ConversionRatio.To]
        public string To { get; }

        /// <summary>How many of the target unit make one of the source.</summary>
        public decimal Number { get; }

        /// <summary>The same ratio read backwards.</summary>
        public Ratio Inverted() {
            return new Ratio(To, From, 1m / Number);
        }

    }

    /// <summary>
    ///     The ratios on record, and the composition that having them as data buys.
    /// </summary>
    public sealed class UnitTable {

        private readonly List<Ratio> _ratios = new();

        /// <summary>Records a ratio, and with it the one in the other direction.</summary>
        public void Add(Ratio ratio) {
            _ratios.Add(ratio);
            _ratios.Add(ratio.Inverted());
        }

        /// <summary>
        ///     The factor from one unit to another, composed through intermediate units where no direct ratio
        ///     was stated. Breadth-first, so the shortest chain wins.
        /// </summary>
        public decimal? FactorFrom(string from, string to) {
            if (from == to) {
                return 1m;
            }

            Queue<(string unit, decimal factor)> queue = new();
            HashSet<string>                      seen  = new() { from };
            queue.Enqueue((from, 1m));

            while (queue.Count > 0) {
                (string unit, decimal factor) = queue.Dequeue();
                foreach (Ratio ratio in _ratios) {
                    if (ratio.From != unit || !seen.Add(ratio.To)) {
                        continue;
                    }

                    decimal reached = factor * ratio.Number;
                    if (ratio.To == to) {
                        return reached;
                    }

                    queue.Enqueue((ratio.To, reached));
                }
            }

            return null;
        }

    }

}
