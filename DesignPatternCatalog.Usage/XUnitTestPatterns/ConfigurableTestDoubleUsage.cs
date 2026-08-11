#region Usings declarations

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.ConfigurableTestDoubleSample {

    // Eleven tests weigh a container: over the limit, under it, exactly on it, negative, zero, and so on.
    // Eleven hard-coded weighbridges is eleven near-identical classes and one place to forget when the
    // interface changes.
    //
    // CONFIGURABLE TEST DOUBLE is one class the test sets up.

    public interface IWeighbridge {

        decimal Weigh(string containerNumber);

    }

    /// <summary>
    ///     One weighbridge, told what to answer during fixture setup.
    /// </summary>
    /// <remarks>
    ///     It removes the duplication between eleven near-copies, and its cost is visible in its shape: a
    ///     double with enough knobs becomes a small framework, and a small framework in the test tree is a
    ///     thing that itself wants testing.
    /// </remarks>
    [ConfigurableTestDouble]
    public sealed class ConfigurableWeighbridge : IWeighbridge {

        private decimal _kilos;

        public ConfigurableWeighbridge Returning(decimal kilos) {
            _kilos = kilos;

            return this;
        }

        public decimal Weigh(string containerNumber) {
            return _kilos;
        }

    }
}
