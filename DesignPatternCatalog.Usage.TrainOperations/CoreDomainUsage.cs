#region Usings declarations

using DesignPatternCatalog.DomainDrivenDesign;

#endregion

// Regional rail: why this model gets the best people.
//
// The same assembly that draws the bounded context is also the CORE DOMAIN, and the two annotations say
// different things. The first says "one model applies here". This one says "this is the model worth the
// effort".
//
// What makes path allocation core is that it is where the operator competes. Fitting one more freight path
// into a timetable already dense with commuter services, without breaking a connection or exceeding what a
// section can carry, is the thing this company does better than the operator on the next network — and it
// is the thing no vendor sells. Billing is bought; path allocation is built.
//
// The consequence the annotation is meant to force is about DEPENDENCY. What is core must not be allowed to
// depend on what merely supports it: nothing here may reference the Invoicing assembly, because a path is
// allocated on operational grounds and would silently start being allocated on billing grounds the day a
// tariff appeared in this code. That is a rule an architecture test can check, and this annotation is what
// gives it something to range over — it is the difference between "we all know Train Operations is the
// important one" and a build that fails when the dependency appears.
//
// The two annotations are also independent in principle: a bounded context is very often not the core
// domain, as the Invoicing assembly shows.

[assembly: CoreDomain]

namespace DesignPatternCatalog.Usage.TrainOperations.CoreDomainSample {

    #region Usings declarations

    using RailNetwork.SharedKernelSample;

    #endregion

    /// <summary>
    ///     The right to run one train over one section within one minute — what the whole model is about.
    /// </summary>
    public sealed class TrainPath {

        public TrainPath(SectionId section, TimeOnly entry, TimeOnly exit) {
            Section = section;
            Entry   = entry;
            Exit    = exit;
        }

        public SectionId Section { get; }
        public TimeOnly  Entry   { get; }
        public TimeOnly  Exit    { get; }

        /// <summary>
        ///     Two paths conflict when they occupy one section at the same time.
        /// </summary>
        public bool ConflictsWith(TrainPath other) {
            return Section == other.Section && Entry < other.Exit && other.Entry < Exit;
        }

    }

}
