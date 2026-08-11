#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.OutcomeAndStartFunctionsSample {

    // A hull thickness reading below the class minimum starts a plating renewal, and the renewal is expected to
    // bring the reading back above it. Both of those are rules about the business, and both usually end up as
    // an `if` in whatever code happened to be reading the gauge.
    //
    // OUTCOME AND START FUNCTIONS puts them at the knowledge level: what an observation sets going, and what an
    // action is expected to produce. A surveyor can then change the threshold without a deployment, and a
    // completed action can be checked against what it was for.

    /// <summary>A function stated at the knowledge level.</summary>
    /// <remarks>
    ///     An object so that what the business expects can be configured and inspected rather than compiled in.
    /// </remarks>
    [OutcomeAndStartFunctions.KnowledgeFunction]
    public abstract class SurveyRule {

        protected SurveyRule(IReadOnlyList<string> arguments) { Arguments = arguments; }

        /// <summary>
        ///     What the function ranges over.
        /// </summary>
        /// <remarks>
        ///     Named on the function rather than assumed by its caller, so a function says for itself what it
        ///     needs.
        /// </remarks>
        [OutcomeAndStartFunctions.Arguments]
        public IReadOnlyList<string> Arguments { get; }

    }

    /// <summary>The observation an action produced.</summary>
    /// <remarks>
    ///     An observation like any other, which is what lets an outcome be the trigger of the next plan.
    /// </remarks>
    [OutcomeAndStartFunctions.Outcome]
    public sealed class ThicknessReading {

        public ThicknessReading(string plate, decimal millimetres, DateOnly on) {
            Plate       = plate;
            Millimetres = millimetres;
            On          = on;
        }

        public string Plate { get; }

        public decimal Millimetres { get; }

        public DateOnly On { get; }

    }

    /// <summary>What an action is expected to bring about.</summary>
    /// <remarks>
    ///     Stating the target makes the outcome checkable against what was intended, which is what turns a
    ///     completed action into evidence rather than a tick.
    /// </remarks>
    [OutcomeAndStartFunctions.OutcomeFunction]
    public sealed class ExpectedThickness : SurveyRule {

        public ExpectedThickness(decimal target)
            : base(new[] { "hull thickness" }) {
            Target = target;
        }

        public decimal Target { get; }

        public bool Achieved(ThicknessReading reading) => reading.Millimetres >= Target;

    }

    /// <summary>What observation sets a plan going, and which procedure it indicates.</summary>
    /// <remarks>
    ///     The reason a plan can begin because of something observed rather than because somebody noticed.
    /// </remarks>
    [OutcomeAndStartFunctions.StartFunction(Outcome = typeof(ThicknessReading))]
    public sealed class RenewalTrigger : SurveyRule {

        public RenewalTrigger(decimal classMinimum, string indicatedProtocol)
            : base(new[] { "hull thickness" }) {
            ClassMinimum      = classMinimum;
            IndicatedProtocol = indicatedProtocol;
        }

        public decimal ClassMinimum { get; }

        /// <summary>"Plating renewal". The procedure the reading indicates.</summary>
        public string IndicatedProtocol { get; }

        public bool Starts(ThicknessReading reading) => reading.Millimetres < ClassMinimum;

    }

}
