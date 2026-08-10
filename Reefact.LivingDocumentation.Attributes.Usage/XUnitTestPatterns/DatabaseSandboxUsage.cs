#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.DatabaseSandboxSample {

    // Four developers and a build agent all ran the integration suite against the same terminal database. It
    // worked for a year, because they never ran it at the same time.
    //
    // DATABASE SANDBOX gives each of them their own.

    /// <summary>
    ///     Hands a run its own schema.
    /// </summary>
    /// <remarks>
    ///     What makes database tests runnable in parallel and on a laptop at the same time as on the build.
    ///     Its absence is not a defect anybody sees until two runs collide, which is why saying it is present
    ///     is worth a line.
    /// </remarks>
    [DatabaseSandbox]
    public sealed class PerRunTerminalSchema {

        public string ConnectionStringFor(string runId) {
            return $"Server=integration;Database=terminal_{runId}";
        }

    }
}
