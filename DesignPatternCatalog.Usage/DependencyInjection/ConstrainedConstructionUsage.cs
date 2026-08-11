#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.DependencyInjection;

#endregion

namespace DesignPatternCatalog.Usage.DependencyInjection.ConstrainedConstructionSample {

    // The station's audio processors — the compressor, the de-esser, the loudness limiter the regulator
    // requires — are loaded by name from a configuration file, so that the engineer can reorder the chain
    // without a deployment. The loader calls Activator.CreateInstance, which means every processor must
    // have a parameterless constructor, which means none of them can declare what it needs.
    //
    // The limiter needs the regulator's current loudness target, which changes twice a year. It gets it
    // from a static, because its constructor is not allowed to ask for it.
    //
    // That is the shape this annotation names, and the constructor is where it sits: the signature is
    // imposed from outside, so its emptiness is not evidence that there is nothing to supply. Reading the
    // constructor tells you nothing, which is precisely the problem.

    public interface IAudioProcessor {

        string Process(string block);

    }

    /// <summary>
    ///     Keeps the station inside the regulator's loudness limit.
    /// </summary>
    public sealed class LoudnessLimiter : IAudioProcessor {

        private readonly decimal _target;

        /// <remarks>
        ///     Parameterless because <see cref="ProcessorChainLoader" /> requires it, not because this class
        ///     needs nothing. It needs the loudness target, and takes it from a static instead — so the
        ///     honest reading of this constructor is *the dependencies arrive by another route*, which no
        ///     signature anywhere states.
        ///     <para>
        ///         Annotating it is what makes the loader's constraint visible from the class it constrains.
        ///         Without it, a reader wonders why a class with an obvious dependency declares none, and
        ///         the answer is three files away.
        ///     </para>
        /// </remarks>
        [ConstrainedConstruction]
        public LoudnessLimiter() {
            _target = RegulatorSettings.LoudnessTarget;
        }

        public string Process(string block) {
            return $"{block}@{_target}";
        }

    }

    /// <summary>
    ///     Where the limiter gets what its constructor could not ask for.
    /// </summary>
    public static class RegulatorSettings {

        public static decimal LoudnessTarget { get; set; } = -23.0m;

    }

    /// <summary>
    ///     Builds the processing chain from the engineer's configuration file.
    /// </summary>
    /// <remarks>
    ///     This is the participant that imposes the constraint, and it is not annotated — the entry holds
    ///     one role, on the constructor, because that is the declaration the constraint lands on. The
    ///     loader is ordinary reflection doing what it was asked to do.
    /// </remarks>
    public sealed class ProcessorChainLoader {

        public IReadOnlyList<IAudioProcessor> Load(IEnumerable<Type> configured) {
            List<IAudioProcessor> chain = new List<IAudioProcessor>();
            foreach (Type processor in configured) {
                chain.Add((IAudioProcessor)Activator.CreateInstance(processor)!);
            }

            return chain;
        }

    }

}
