#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.DependencyInjection;

#endregion

namespace DesignPatternCatalog.Usage.DependencyInjection.ScopedLifestyleSample {

    // Producers edit next week's schedule through a web front end. Moving a programme touches four tables,
    // and either all four move or none does — so everything serving one request shares one transaction,
    // and the next request gets its own.
    //
    // That is one instance per scope, shared inside it, and it is the lifestyle that carries the most
    // obligations while looking the simplest.
    //
    // The failure it cannot prevent on its own is reaching it from outside a scope, and the station has hit
    // it: a background job that recalculates repeat fees was written by copying an editor class, resolved
    // the transaction outside any request, and wrote four tables under a transaction nobody would ever
    // commit. It failed silently for a week — the rows were there in the job's own reads.

    /// <summary>
    ///     The unit of work for one schedule edit.
    /// </summary>
    /// <remarks>
    ///     One per request, shared by everything serving it, and a different one for the next. So **it need
    ///     not be safe against the whole application** — only against whatever runs concurrently inside a
    ///     single request, which for this front end is nothing, and that is why it holds a plain list.
    ///     <para>
    ///         Two failures the lifestyle does not prevent, both of which have happened here. Reaching it
    ///         from outside a scope, which is what the repeat-fee job did. And a longer-lived class holding
    ///         on to it — a singleton that captured this would use one request's transaction for every
    ///         request after it, which is the shape the singleton entry's second obligation is about.
    ///     </para>
    /// </remarks>
    [ScopedLifestyle]
    public sealed class ScheduleEditUnitOfWork {

        private readonly List<string> _pending = new List<string>();

        public void Stage(string change) {
            _pending.Add(change);
        }

        public IReadOnlyList<string> Commit() {
            List<string> committed = new List<string>(_pending);
            _pending.Clear();

            return committed;
        }

    }

}
