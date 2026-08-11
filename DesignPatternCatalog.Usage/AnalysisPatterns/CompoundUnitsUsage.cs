#region Usings declarations

using System.Collections.Generic;
using System.Linq;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.CompoundUnitsSample {

    // A hospital's infusion pumps. The units on a prescription are almost never atomic: 125 mL/h of saline,
    // 0.05 mcg/kg/min of noradrenaline, 4 mg/kg over 30 min of an antibiotic.
    //
    // A model that keeps units as strings can store all of those and reason about none of them. It cannot tell
    // that mcg/kg/min and mg/kg/h are the same dimension, so it cannot convert a prescription written in one to
    // a pump programmed in the other — and that conversion, done by hand at three in the morning, is a
    // well-documented category of fatal error.
    //
    // COMPOUND UNITS makes the unit a structure: unit references, each with a power. Figure 3.4's constraint is
    // what keeps it honest — a compound unit must hold more than one reference, or one whose power is not one,
    // otherwise it is an atomic unit wearing a costume.
    //
    // The role sits on the supertype as well, because that is what the pattern buys: a quantity holds a unit
    // without knowing which kind, and dimensional comparison is asked of the same interface either way.

    /// <summary>
    ///     What a quantity is measured in, atomic or compound.
    /// </summary>
    [CompoundUnits.Unit]
    public interface IUnit {

        /// <summary>The dimension, as atomic units to powers — what makes two spellings comparable.</summary>
        IReadOnlyDictionary<string, int> Dimension { get; }

    }

    /// <summary>
    ///     A unit that decomposes no further.
    /// </summary>
    [CompoundUnits.AtomicUnit]
    public sealed class AtomicUnit : IUnit {

        public AtomicUnit(string symbol) {
            Symbol = symbol;
        }

        /// <summary>Its symbol: g, m, s, L.</summary>
        public string Symbol { get; }

        /// <inheritdoc />
        public IReadOnlyDictionary<string, int> Dimension => new Dictionary<string, int> { [Symbol] = 1 };

    }

    /// <summary>
    ///     One unit raised to a power.
    /// </summary>
    /// <remarks>
    ///     The power is what makes the model reason rather than concatenate: minus one is a reciprocal, two is
    ///     an area, and a string like "mL/h" says neither.
    /// </remarks>
    [CompoundUnits.UnitReference]
    public sealed class UnitReference {

        public UnitReference(IUnit unit, int power) {
            Unit  = unit;
            Power = power;
        }

        /// <summary>The unit referred to.</summary>
        public IUnit Unit { get; }

        /// <summary>The power it is raised to. Negative for a denominator.</summary>
        public int Power { get; }

    }

    /// <summary>
    ///     A unit made of unit references.
    /// </summary>
    /// <remarks>
    ///     Enforces figure 3.4's constraint on construction, because a compound unit of one reference to the
    ///     power of one is a duplicate spelling and duplicate spellings are what break comparison.
    /// </remarks>
    [CompoundUnits.CompoundUnit]
    public sealed class CompoundUnit : IUnit {

        private readonly List<UnitReference> _references;

        public CompoundUnit(string symbol, params UnitReference[] references) {
            if (references.Length == 0) {
                throw new System.ArgumentException("a compound unit needs at least one reference", nameof(references));
            }

            if (references.Length == 1 && references[0].Power == 1) {
                throw new System.ArgumentException(
                    "a single reference to the power of one is an atomic unit spelled twice", nameof(references));
            }

            Symbol      = symbol;
            _references = new List<UnitReference>(references);
        }

        /// <summary>How it is written on a prescription: mL/h, mcg/kg/min.</summary>
        public string Symbol { get; }

        /// <summary>What it is made of.</summary>
        public IReadOnlyList<UnitReference> References => _references;

        /// <inheritdoc />
        public IReadOnlyDictionary<string, int> Dimension {
            get {
                Dictionary<string, int> dimension = new();
                foreach (UnitReference reference in _references) {
                    foreach (KeyValuePair<string, int> part in reference.Unit.Dimension) {
                        dimension.TryGetValue(part.Key, out int power);
                        dimension[part.Key] = power + part.Value * reference.Power;
                    }
                }

                return dimension.Where(p => p.Value != 0).ToDictionary(p => p.Key, p => p.Value);
            }
        }

        /// <summary>
        ///     Whether two units measure the same kind of thing, whatever they are called. This is the question
        ///     a string cannot answer, and the one a nurse needs answered before reprogramming a pump.
        /// </summary>
        public static bool SameDimension(IUnit left, IUnit right) {
            IReadOnlyDictionary<string, int> a = left.Dimension;
            IReadOnlyDictionary<string, int> b = right.Dimension;

            return a.Count == b.Count && a.All(p => b.TryGetValue(p.Key, out int power) && power == p.Value);
        }

    }

}
