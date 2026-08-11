#region Usings declarations

using System;

using DesignPatternCatalog.MicroservicesPatterns;

#endregion

namespace DesignPatternCatalog.Usage.MicroservicesPatterns.ExceptionTrackingSample {

    // The reading importer had been failing on one malformed file a night for five weeks. Every failure was
    // caught, logged and counted; nobody was reading that log.
    //
    // EXCEPTION TRACKING reports faults to something that de-duplicates them and tells somebody. The
    // annotation marks the boundary between a fault that reaches a human and a fault that reaches a file.

    /// <summary>
    ///     Sends faults where a human will see them.
    /// </summary>
    /// <remarks>
    ///     What this annotation says is that a fault reaching here reaches a person. A swallowed exception
    ///     and a reported one are one line apart and read almost identically, which is why the difference
    ///     is worth stating rather than inferring from a <c>catch</c> block.
    /// </remarks>
    [ExceptionTracking]
    public interface IFaultReporter {

        void Report(Exception exception, string operation);

    }
}
