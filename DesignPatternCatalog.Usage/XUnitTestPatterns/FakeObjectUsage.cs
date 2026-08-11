#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.FakeObjectSample {

    // The stowage plan store keeps several megabytes per vessel and is backed by a blob store. The planning
    // steps that read it back need it to actually work — a stub that returns the same plan for every key
    // would hide exactly the bug worth catching, a plan fetched under the wrong reference.
    //
    // FAKE OBJECT works, and costs nothing.

    public interface IStowagePlanStore {

        void Put(Guid reference, string planXml);

        string Get(Guid reference);

    }

    /// <summary>
    ///     A working store, in memory.
    /// </summary>
    /// <remarks>
    ///     The only kind of double with behaviour of its own, which is the only kind that can have bugs: a
    ///     fake that has drifted from the thing it replaces — case-sensitive here, case-insensitive in
    ///     production — makes every test using it lie, and they all keep passing.
    /// </remarks>
    [FakeObject]
    public sealed class InMemoryStowagePlanStore : IStowagePlanStore {

        private readonly Dictionary<Guid, string> _plans = new Dictionary<Guid, string>();

        public void Put(Guid reference, string planXml) {
            _plans[reference] = planXml;
        }

        public string Get(Guid reference) {
            return _plans[reference];
        }

    }
}
