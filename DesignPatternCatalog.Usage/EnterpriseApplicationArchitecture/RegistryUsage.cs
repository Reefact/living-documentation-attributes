#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.RegistrySample {

    // Laboratory information system: finding the driver for the analyser a sample came off.
    //
    // A result arrives from bench three. Sixteen call sites, deep inside result validation, need the driver
    // that produced it in order to ask what its reportable range is. Threading a driver collection through
    // sixteen call chains would put a parameter on forty methods that have no interest in it.
    //
    // A REGISTRY is the agreed place to look one up.
    //
    // The pattern comes with its cost attached, and the cost is that it is global. What matters is not
    // avoiding that — it is being deliberate about the SCOPE, which is the part usually left unsaid. This
    // one is per process, because a lab's bench does not change while the software runs, and it is stated
    // in the type rather than left to be discovered.
    //
    // The failure a stated scope prevents is specific: a registry populated by one test and read by the
    // next is how a suite starts passing in one order and failing in another. That is a real bug, it is
    // hard to find, and the annotation is what lets a reviewer ask the question — what scope is this? —
    // without reading the implementation.

    /// <summary>
    ///     The analyser drivers this process knows about, found by bench.
    /// </summary>
    /// <remarks>
    ///     Scope is per PROCESS and deliberately so: the bench is fixed for the lifetime of the service.
    ///     Anything request-scoped must not be put here — a registry with two scopes has one bug.
    /// </remarks>
    [Registry]
    public static class AnalyserRegistry {

        private static readonly Dictionary<int, string> DriversByBench = new();

        #region Statics members declarations

        public static void Register(int bench, string driverKey) {
            DriversByBench[bench] = driverKey;
        }

        public static string? DriverFor(int bench) {
            return DriversByBench.TryGetValue(bench, out string? key) ? key : null;
        }

        #endregion

    }

}
