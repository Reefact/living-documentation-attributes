#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.MessageTranslatorSample {

    // The shipping line speaks EDIFACT CODECO. The terminal speaks its own JSON. Neither will change, and
    // neither should have to learn the other.
    //
    // MESSAGE TRANSLATOR changes the format and nothing else — not the route, not the destination.

    /// <summary>
    ///     Changes a message's format and not its route.
    /// </summary>
    /// <remarks>
    ///     The counterpart of a router, and keeping the two apart is what lets a pipeline be reasoned about:
    ///     one step changes where, the other changes what.
    /// </remarks>
    [MessageTranslator]
    public sealed class CodecoToTerminalJson {

        public string Translate(string edifact) {
            // ... one format in, another out; the destination is somebody else's decision
            return "{}";
        }

    }
}
