#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.DependencyInjectionSample {

    // The gate service reads the clock, calls the container registry and publishes to the bus. Testing "a
    // booking that expired at midnight" means moving the machine's clock, which is where that test stopped
    // being written.
    //
    // DEPENDENCY INJECTION hands it the three from outside instead.

    /// <summary>
    ///     Never obtains its own collaborators.
    /// </summary>
    /// <remarks>
    ///     The most common answer to testability and the one leaving the fewest traces: a class built this
    ///     way looks like any other, and the fact that it was designed to be substitutable is exactly what
    ///     nothing in it says.
    /// </remarks>
    [DependencyInjection]
    public sealed class GateService {

        private readonly IClock _clock;

        public GateService(IClock clock) {
            _clock = clock;
        }

    }

    public interface IClock { }
}
