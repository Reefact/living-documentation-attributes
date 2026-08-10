#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.BackDoorManipulationSample {

    // Arranging "a container held by customs for six days" through the front door means running six days of
    // gate, customs and clock. The test writes the row instead.
    //
    // BACK DOOR MANIPULATION is that shortcut, and it is worth being able to find every one of them.

    /// <summary>
    ///     Writes the hold straight into the database, past the system under test.
    /// </summary>
    /// <remarks>
    ///     Often the only practical way to arrange a state, and always a second definition of the data's
    ///     shape: when the SUT's own writing changes, the back door keeps working and keeps being wrong.
    /// </remarks>
    [BackDoorManipulation]
    public static class CustomsBackDoor {

        public static void InsertHold(string containerNumber, int daysAgo) { }

    }
}
