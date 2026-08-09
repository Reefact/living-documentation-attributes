#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.AnalysisPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AnalysisPatterns.MeasurementProtocolSample {

    // The same co-operative, one level down. Cost per litre is not measured; it is cost divided by volume. Two
    // people can compute it from the same ledger and disagree, because one of them included freight.
    //
    // Storing the answer loses the argument. MEASUREMENT PROTOCOL stores the method: what the figure takes, what
    // it yields, and the formula between them — so the number can be recomputed, and its inputs named when the
    // board asks why it moved.

    /// <summary>
    ///     What kind of figure this is.
    /// </summary>
    /// <remarks>
    ///     Configured, not declared. Chapter 3's phenomenon type, reached from here as a result and as an input.
    /// </remarks>
    public sealed class FigureType {

        public FigureType(string name, string unit) {
            Name = name;
            Unit = unit;
        }

        public string Name { get; }

        public string Unit { get; }

    }

    /// <summary>
    ///     How a figure of one kind is arrived at.
    /// </summary>
    /// <remarks>
    ///     The type object. It names the figure type it results in, so a protocol is enough to produce a figure
    ///     and not merely enough to tell two figures apart.
    /// </remarks>
    [MeasurementProtocol.MeasurementProtocol]
    public abstract class FigureProtocol {

        protected FigureProtocol(string name, FigureType resultType) {
            Name       = name;
            ResultType = resultType;
        }

        public string Name { get; }

        public FigureType ResultType { get; }

    }

    /// <summary>
    ///     A figure somebody read off something.
    /// </summary>
    /// <remarks>
    ///     No inputs. This is where the numbers every calculation stands on enter the model — a weighbridge
    ///     docket, a general-ledger balance.
    /// </remarks>
    [MeasurementProtocol.SourceMeasurementProtocol]
    public sealed class ReadFromLedger : FigureProtocol {

        public ReadFromLedger(string name, FigureType resultType, string account)
            : base(name, resultType) {
            Account = account;
        }

        /// <summary>The account read. Naming it is what makes the figure auditable.</summary>
        public string Account { get; }

    }

    /// <summary>
    ///     A figure computed from other figures by a formula this protocol holds.
    /// </summary>
    /// <remarks>
    ///     Its input types are ordered, because a formula that divides cares which operand is which.
    /// </remarks>
    [MeasurementProtocol.CalculatedMeasurementProtocol]
    public sealed class DerivedFigure : FigureProtocol {

        public DerivedFigure(string name, FigureType resultType, IReadOnlyList<FigureType> inputTypes, IFormula formula)
            : base(name, resultType) {
            InputTypes = inputTypes;
            Formula    = formula;
        }

        public IReadOnlyList<FigureType> InputTypes { get; }

        public IFormula Formula { get; }

    }

    /// <summary>
    ///     The arithmetic, as an object.
    /// </summary>
    /// <remarks>
    ///     Outside the protocol so that one formula serves several, and so that a new protocol is configuration
    ///     rather than a class.
    /// </remarks>
    [MeasurementProtocol.Method]
    public interface IFormula {

        decimal Apply(IReadOnlyList<decimal> inputs);

    }

    /// <summary>
    ///     Inputs of different kinds, combined.
    /// </summary>
    /// <remarks>
    ///     Cost per litre from a cost and a volume. Heterogeneous inputs are what separate this from a
    ///     comparison.
    /// </remarks>
    [MeasurementProtocol.CausalCalculation]
    public sealed class Ratio : IFormula {

        public decimal Apply(IReadOnlyList<decimal> inputs) {
            if (inputs.Count != 2) { throw new ArgumentException("a ratio takes two inputs", nameof(inputs)); }
            return inputs[1] == 0m ? 0m : inputs[0] / inputs[1];
        }

    }

    /// <summary>
    ///     Two inputs of the same kind, compared.
    /// </summary>
    /// <remarks>
    ///     This season against last. Exactly two, and both of one figure type — the cardinality is the
    ///     assertion.
    /// </remarks>
    [MeasurementProtocol.ComparativeCalculation]
    public sealed class Movement : IFormula {

        public decimal Apply(IReadOnlyList<decimal> inputs) {
            if (inputs.Count != 2) { throw new ArgumentException("a movement takes two inputs", nameof(inputs)); }
            return inputs[0] - inputs[1];
        }

    }

    /// <summary>
    ///     One input of the result's own kind, summed up an axis.
    /// </summary>
    /// <remarks>
    ///     A figure for New Zealand from the figures for its regions. Input type equals result type, which is
    ///     what lets it compose up a hierarchy without a case per level.
    /// </remarks>
    [MeasurementProtocol.DimensionCombination]
    public sealed class RollUp : IFormula {

        public decimal Apply(IReadOnlyList<decimal> inputs) {
            decimal total = 0m;
            foreach (decimal input in inputs) { total += input; }
            return total;
        }

    }

    /// <summary>
    ///     A figure produced under a calculated protocol.
    /// </summary>
    /// <remarks>
    ///     An ordinary figure to everything that reads it, which is what keeps a derived number comparable with
    ///     an observed one.
    /// </remarks>
    [MeasurementProtocol.CalculatedMeasurement(CalculatedMeasurementProtocol = typeof(DerivedFigure))]
    public sealed class CalculatedFigure {

        public CalculatedFigure(DerivedFigure protocol, decimal amount, IReadOnlyList<CalculatedFigure> sources) {
            Protocol = protocol;
            Amount   = amount;
            Sources  = sources;
        }

        public DerivedFigure Protocol { get; }

        public decimal Amount { get; }

        /// <summary>
        ///     What this figure was computed from.
        /// </summary>
        /// <remarks>
        ///     Kept rather than discarded once the arithmetic is done, which is the difference between a figure
        ///     that can be re-explained and a number nobody can defend.
        /// </remarks>
        [MeasurementProtocol.Sources]
        public IReadOnlyList<CalculatedFigure> Sources { get; }

    }

}
