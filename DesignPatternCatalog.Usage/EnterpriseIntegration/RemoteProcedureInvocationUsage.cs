#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.RemoteProcedureInvocationSample {

    // Before a crane lifts a container onto a ship, the terminal asks the line whether the container is
    // released — no hold, paid, documents in order. That answer is needed now: the crane is waiting.
    //
    // REMOTE PROCEDURE INVOCATION is right precisely because the caller must not proceed without the answer.
    // The coupling in time is the point, not an oversight.

    /// <summary>
    ///     Asks the shipping line whether a container may be loaded.
    /// </summary>
    /// <remarks>
    ///     The caller waits and the callee must be up. That is what buys an answer before the lift, and it is
    ///     why the same shape would be wrong for anything that can be answered later.
    /// </remarks>
    [RemoteProcedureInvocation]
    public interface IReleaseCheck {

        bool IsReleased(string containerNumber);

    }
}
