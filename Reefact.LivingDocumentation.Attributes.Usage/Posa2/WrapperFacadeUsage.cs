#region Usings declarations

using System.Runtime.InteropServices;

using Reefact.LivingDocumentation.Attributes.Posa2;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.Posa2.WrapperFacadeSample {

    // The harbour's AIS receiver ships a C library: an integer handle, an out parameter for the message,
    // a return code that is zero for success on Linux and non-zero for one particular error on Windows,
    // and a free() the caller must remember.
    //
    // Calls to it were spread across nine files. The Windows quirk was handled in four of them and not
    // in the other five, and the free() was missed in the retry path — which leaked eleven bytes per
    // message and took four months of uptime to notice.
    //
    // WRAPPER FACADE puts the quirks in one class. The nine files now call a method that returns a
    // string or throws, and the platform difference exists in one place where a second platform will
    // find it.

    /// <summary>
    ///     Reads AIS position reports from the harbour receiver.
    /// </summary>
    /// <remarks>
    ///     One place holds the receiver's quirks — the return-code convention, the ownership of the
    ///     buffer, the difference between the two platforms. That is what makes them reviewable: a change
    ///     to the convention is a change to this file, not a search across nine.
    /// </remarks>
    [WrapperFacade.WrapperFacade]
    public sealed class AisReceiver : IDisposable {

        private readonly int _handle;

        public AisReceiver(string device) {
            _handle = ais_open(device);
            if (_handle <= 0) { throw new IOException($"the receiver at {device} did not open"); }
        }

        public string? ReadReport() {
            IntPtr buffer = IntPtr.Zero;
            try {
                // Zero is success on Linux; on Windows the library also returns 1 for "no message yet",
                // which is not an error. That sentence used to live in four of nine call sites.
                int code = ais_read(_handle, out buffer, out int length);
                if (code != 0 && !(OperatingSystem.IsWindows() && code == 1)) {
                    throw new IOException($"the receiver reported {code}");
                }

                return length == 0 ? null : Marshal.PtrToStringUTF8(buffer, length);
            } finally {
                if (buffer != IntPtr.Zero) { ais_free(buffer); }
            }
        }

        public void Dispose() {
            ais_close(_handle);
        }

        /// <remarks>
        ///     Called from this facade and nowhere else. That is what the annotation claims, and it is the
        ///     claim that was broken for four months: a call added in another file compiles, links and
        ///     works, and takes the platform quirk and the free() with it.
        /// </remarks>
        [WrapperFacade.Functions(WrapperFacade = typeof(AisReceiver))]
        [DllImport("libais", EntryPoint = "ais_open")]
        private static extern int ais_open(string device);

        /// <inheritdoc cref="ais_open" />
        [WrapperFacade.Functions(WrapperFacade = typeof(AisReceiver))]
        [DllImport("libais", EntryPoint = "ais_read")]
        private static extern int ais_read(int handle, out IntPtr message, out int length);

        /// <inheritdoc cref="ais_open" />
        [WrapperFacade.Functions(WrapperFacade = typeof(AisReceiver))]
        [DllImport("libais", EntryPoint = "ais_free")]
        private static extern void ais_free(IntPtr message);

        /// <inheritdoc cref="ais_open" />
        [WrapperFacade.Functions(WrapperFacade = typeof(AisReceiver))]
        [DllImport("libais", EntryPoint = "ais_close")]
        private static extern void ais_close(int handle);

    }

}
