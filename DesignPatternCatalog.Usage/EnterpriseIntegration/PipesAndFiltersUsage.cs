#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.PipesAndFiltersSample {

    // An inbound EDI manifest has to be decrypted, de-duplicated, validated against the booking list and only
    // then handed to the yard planner. Written as one method, the four concerns are impossible to test apart
    // and the day somebody needs validation without de-duplication the method grows a flag.
    //
    // PIPES AND FILTERS makes each step independent and the order a fact stated in one place.

    /// <summary>
    ///     One processing step.
    /// </summary>
    /// <remarks>
    ///     It knows nothing of what precedes or follows it, which is the property that lets the sequence be
    ///     rearranged without editing a step.
    /// </remarks>
    [PipesAndFilters.Filter]
    public interface IManifestFilter {

        string Process(string message);

    }

    /// <summary>
    ///     The channel joining two steps.
    /// </summary>
    /// <remarks>
    ///     A participant rather than a method call, which is what decouples the steps in time.
    /// </remarks>
    [PipesAndFilters.Pipe]
    public interface IManifestPipe {

        void Put(string message);

        string? Take();

    }

    /// <summary>
    ///     The assembled sequence.
    /// </summary>
    /// <remarks>
    ///     The only participant that knows the order, so the order is stated once instead of being implied by
    ///     who calls whom.
    /// </remarks>
    [PipesAndFilters.Pipeline(Filter = typeof(IManifestFilter))]
    public sealed class ManifestPipeline {

        private readonly IReadOnlyList<IManifestFilter> _steps;

        public ManifestPipeline(IReadOnlyList<IManifestFilter> steps) { _steps = steps; }

        public string Run(string message) {
            foreach (IManifestFilter step in _steps) { message = step.Process(message); }

            return message;
        }

    }
}
