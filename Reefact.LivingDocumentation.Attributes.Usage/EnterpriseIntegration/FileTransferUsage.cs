#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.FileTransferSample {

    // A container terminal and the customs authority. Customs will not open a socket to a terminal, and the
    // terminal will not be given a login to customs: what crosses is a file, dropped on an SFTP server at
    // 04:00 with the previous day's declarations.
    //
    // FILE TRANSFER is the cheapest integration there is, and the slowest. Nothing is shared but a format —
    // and nothing is known until somebody writes a file and somebody else notices.

    /// <summary>
    ///     Writes the day's declarations where customs will find them.
    /// </summary>
    /// <remarks>
    ///     The two systems share no technology at all, which is the whole benefit. The cost is timeliness: a
    ///     declaration lodged at 04:01 waits a day.
    /// </remarks>
    [FileTransfer]
    public sealed class DeclarationFileExport {

        public string WriteFor(DateOnly day, IReadOnlyList<string> declarations) {
            string path = $"/outbound/customs-{day:yyyyMMdd}.edi";
            // ... writes one line per declaration, in the agreed layout
            return path;
        }

    }
}
