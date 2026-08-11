#region Usings declarations

using System.Text;

using Reefact.LivingDocumentation.Attributes.DependencyInjection;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.DependencyInjection.TransientLifestyleSample {

    // Building the hourly regulator return means accumulating three thousand play-out lines into one
    // document. The builder that does it is stateful by design — it is a buffer with a pen — and giving it
    // a longer life than one return would put January's lines in February's document.
    //
    // So it is transient: a new one for each consumer that asks, and no reuse. That is the easy half.
    //
    // The half worth annotating is what happens because it is IDisposable. The container hands it out and
    // then forgets it, so the disposal is somebody's job and there is no compiler and no container that
    // will say whose. This one is disposed by the caller, in a using; the version before it was resolved
    // and never disposed, and the station leaked a file handle per hour for five months.

    /// <summary>
    ///     Accumulates one regulator return.
    /// </summary>
    /// <remarks>
    ///     A fresh instance for every return, so **it may hold state freely** — nothing of it survives the
    ///     consumer that received it, which is the licence a transient lifestyle grants and the reason this
    ///     class can be written as a buffer rather than as a function.
    ///     <para>
    ///         The difficulty is disposal. A container generally does not track what it hands out
    ///         transiently, so a disposable transient is a leak nothing reports: no exception, no failing
    ///         test, just a handle per hour. Whoever asks for one owns it, and this remark is where that is
    ///         written down — the type says `IDisposable` and says nothing about who calls it.
    ///     </para>
    /// </remarks>
    [TransientLifestyle]
    public sealed class RegulatorReturnBuilder : IDisposable {

        private readonly StringBuilder _lines = new StringBuilder();

        private bool _closed;

        public void Add(string trackId, int seconds) {
            if (_closed) { throw new ObjectDisposedException(nameof(RegulatorReturnBuilder)); }

            _lines.Append(trackId).Append(';').Append(seconds).Append('\n');
        }

        public string Build() {
            return _lines.ToString();
        }

        public void Dispose() {
            _closed = true;
        }

    }

}
