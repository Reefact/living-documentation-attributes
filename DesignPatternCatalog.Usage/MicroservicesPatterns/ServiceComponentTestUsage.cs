#region Usings declarations

using DesignPatternCatalog.MicroservicesPatterns;

#endregion

namespace DesignPatternCatalog.Usage.MicroservicesPatterns.ServiceComponentTestSample {

    // Metering validates a reading against the substation's declared capacity, which it gets from the grid.
    // Running the grid to test that takes a database, a message broker and four minutes; standing in for it
    // takes a line.
    //
    // SERVICE COMPONENT TEST runs metering and nothing else. The suite is fast, cheap and reliable, and the
    // work states the trade rather than hiding it: everything outside the annotated boundary is a claim
    // resting on a stand-in that somebody has to keep honest — which is the job of the contract test.

    /// <summary>
    ///     The one service actually running.
    /// </summary>
    /// <remarks>
    ///     The boundary of what the suite has really tested. Anything reached beyond this is replaced, so
    ///     this annotation is where a reader learns how much of a green run is real.
    /// </remarks>
    [ServiceComponentTest.ServiceUnderTest]
    public sealed class MeteringService {

        private readonly IGridCapacity _grid;

        public MeteringService(IGridCapacity grid) {
            _grid = grid;
        }

        public bool Accepts(string substation, decimal kilowattHours) => kilowattHours <= _grid.DeclaredCapacityAt(substation);

    }

    /// <summary>
    ///     The grid, which does not run in this suite.
    /// </summary>
    public interface IGridCapacity {

        decimal DeclaredCapacityAt(string substation);

    }

    /// <summary>
    ///     Metering, exercised on its own.
    /// </summary>
    /// <remarks>
    ///     The stand-in below is deliberately not annotated. Naming the five kinds of double is
    ///     <c>XUnitTestPatterns</c>'s job and it does it in five entries; a sixth, vaguer one here would say
    ///     less about the same class. What this catalogue adds is the boundary, not the double.
    /// </remarks>
    [ServiceComponentTest.ServiceComponentTest(ServiceUnderTest = typeof(MeteringService))]
    public sealed class MeteringComponentTest {

        private sealed class GridStub : IGridCapacity {

            public decimal DeclaredCapacityAt(string substation) => 100m;

        }

        public bool RefusesAReadingAboveDeclaredCapacity() => !new MeteringService(new GridStub()).Accepts("SUB-1", 101m);

    }
}
