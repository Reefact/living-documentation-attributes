#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.IdentityMapSample {

    // Regional library: why one row must never become two objects.
    //
    // A counter session renews three loans for one member. The first renewal loads the member to check the
    // fine allowance. The second loads it again. Without an IDENTITY MAP there are now two Member objects
    // for one row — and the moment either is changed, the session holds two different truths about the same
    // person. Whichever is written second wins, silently, and the first clerk's change is gone.
    //
    // That is the point worth holding on to: this pattern is about CORRECTNESS first. It does save a query,
    // and it is often introduced for that, but the bug it prevents is the expensive one — a lost update
    // that no error message ever mentions.
    //
    // Its scope is a session, and that is not a detail. An identity map that outlived one would be a cache,
    // with a cache's whole different problem: knowing when what it holds has gone stale. This one is
    // discarded when the unit of work ends, which is why it never has to answer that question.

    /// <summary>
    ///     The objects this session has already loaded, so that a second request for one returns the first.
    /// </summary>
    /// <remarks>
    ///     Consulted before every load, and never shared between sessions.
    /// </remarks>
    [IdentityMap]
    public sealed class SessionIdentityMap {

        private readonly Dictionary<(Type, long), object> _loaded = new();

        public T? Get<T>(long id) where T : class {
            return _loaded.TryGetValue((typeof(T), id), out object? found) ? (T)found : null;
        }

        public void Put<T>(long id, T instance) where T : class {
            _loaded[(typeof(T), id)] = instance;
        }

    }

}
