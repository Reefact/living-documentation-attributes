#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.DependencyInjection;

#endregion

namespace DesignPatternCatalog.Usage.DependencyInjection.SingletonLifestyleSample {

    // The station's track library is forty thousand recordings with their duration, rights holder and
    // territory restrictions. Reading it takes eleven seconds, and every playout decision needs it, so it
    // is read once and shared by everything for as long as the process lives.
    //
    // The annotation is not a description of what the container was told. It is a constraint the class
    // has to satisfy, and it is the only place that constraint is written down: the registration line in
    // the composition root says AddSingleton, and says nothing about what that obliges this class to be.
    //
    // A rule can check the two against each other — every class marked here is registered once, and every
    // class registered once is marked. That is the point of annotating a lifestyle rather than trusting
    // the wiring.

    /// <summary>
    ///     Every recording the station may broadcast.
    /// </summary>
    /// <remarks>
    ///     One instance for the life of the process, so **it is used concurrently and must be safe for
    ///     that** — every gate thread and every scheduled job reads it at once. Nothing here may belong to
    ///     one caller: a field remembering the last query would be a field shared by everybody who ever
    ///     queries.
    ///     <para>
    ///         The second obligation is the one that bites from outside. Everything this class depends on
    ///         outlives every consumer, so a dependency with a shorter life reaching in here is held far
    ///         past the life it was given — a request-scoped connection captured by this class would be
    ///         used long after its request ended. That is why what it takes is a factory rather than the
    ///         thing itself.
    ///     </para>
    /// </remarks>
    [SingletonLifestyle]
    public sealed class TrackLibrary {

        private readonly IReadOnlyDictionary<string, int> _durations;

        public TrackLibrary(Func<IReadOnlyDictionary<string, int>> read) {
            _durations = read();
        }

        public int DurationOf(string trackId) {
            return _durations.TryGetValue(trackId, out int seconds) ? seconds : 0;
        }

    }

}
