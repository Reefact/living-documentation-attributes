#region Usings declarations

using System.Collections.Generic;
using System.Threading;

using Reefact.LivingDocumentation.Attributes.Posa2;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.Posa2.ThreadSpecificStorageSample {

    // Every line the control room writes to the incident log has to carry the call reference it belongs
    // to, and so does every audit record, every dispatch and every recording marker. Threading a call
    // reference through forty methods to reach the four that log was tried and abandoned: the parameter
    // reached thirty-six methods that had no use for it, and the four that did got it wrong twice.
    //
    // A static field was the next attempt. It made every position write the reference of whichever call
    // was answered last, and the log became unreadable during the only hour anybody needed to read it.
    //
    // THREAD-SPECIFIC STORAGE is the shape that is global to read and private to hold: one access point,
    // one object per position, no lock — because nothing another thread can see is being touched.

    /// <summary>
    ///     The call this position is working, reachable from anywhere without being passed anywhere.
    /// </summary>
    /// <remarks>
    ///     Reads exactly like global state and is not, which is the whole value of the pattern and the
    ///     reason a reader needs telling: two positions reading this line get different answers, and that
    ///     is correct.
    /// </remarks>
    [ThreadSpecificStorage.TSObjectProxy]
    public static class CurrentCall {

        /// <remarks>
        ///     One position's own call reference. Nothing serializes access to it because nothing else can
        ///     see it — so handing this value to a background task, where another thread can reach it,
        ///     removes the only guarantee the pattern makes and leaves code that looks unchanged.
        /// </remarks>
        [ThreadSpecificStorage.TSObject(TSObjectProxy = typeof(CurrentCall))]
        private static readonly ThreadLocal<string?> Reference = new ThreadLocal<string?>();

        public static string? Get() {
            return Reference.Value;
        }

        public static void Set(string callReference) {
            Reference.Value = callReference;
        }

    }

    /// <summary>
    ///     The control room's own map from key to this position's value.
    /// </summary>
    /// <remarks>
    ///     On this platform the runtime supplies thread-local storage, so a codebase applying the pattern
    ///     normally has no collection of its own and this role goes unused. It is written here because
    ///     this room does keep one: the shift-handover tooling has to enumerate what every position is
    ///     holding, which the runtime's storage will not tell it.
    /// </remarks>
    [ThreadSpecificStorage.TSObjectCollection(TSObjectProxy = typeof(CurrentCall))]
    public sealed class PositionContext {

        private static readonly ThreadLocal<Dictionary<string, string>> Entries =
            new ThreadLocal<Dictionary<string, string>>(() => new Dictionary<string, string>());

        public static string? Get(string key) {
            return Entries.Value!.TryGetValue(key, out string? value) ? value : null;
        }

        public static void Set(string key, string value) {
            Entries.Value![key] = value;
        }

    }

}
