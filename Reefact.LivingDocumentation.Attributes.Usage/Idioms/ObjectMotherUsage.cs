#region Usings declarations

using Reefact.LivingDocumentation.Attributes.Idioms;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.Idioms.ObjectMotherSample {

    // Hospital admissions: the twenty lines that hide what a test is about.
    //
    // An admission needs a patient with an NHS number, a ward with a bed, a consultant, an arrival time, a
    // triage category and a referral source. A test about discharge planning needs all of it valid and
    // cares about exactly one thing: that the patient arrived more than twenty-one days ago.
    //
    // Written inline, that test opens with twenty lines of construction and closes with one line of intent,
    // and the reader has to diff it against the next test to find out which of the twenty lines matters.
    //
    // An OBJECT MOTHER moves the construction behind a name, and the name is the point. Not
    // NewAdmission(patient, ward, consultant, arrivedAt, triage, referral) — that is a constructor with
    // extra steps. AnAdmissionAwaitingDischargeFor(21) names the SITUATION, so the test reads as the
    // sentence it is testing and the twenty lines stop being visible at all.
    //
    // Two things keep it from rotting into a second domain model:
    //
    //   * every method returns an object that is already valid and already meaningful — a test never has to
    //     finish building one;
    //   * the methods are named for situations the domain recognises, so a new one is added when a new
    //     SITUATION appears, not when a new field does.
    //
    // This is the first pattern in the catalog that lives in test code rather than in production code.
    // Nothing about the vocabulary changes — it is annotated, read back and counted like any other — but it
    // is a precedent, and ADR-0022 records it.

    #region The domain the tests are about

    public sealed record Patient(string NhsNumber, string Name);

    public sealed record Admission(Patient Patient, string Ward, DateOnly ArrivedOn, string TriageCategory);

    #endregion

    /// <summary>
    ///     Admissions for tests, named for the situations that matter.
    /// </summary>
    [ObjectMother]
    public static class Admissions {

        #region Statics members declarations

        /// <summary>
        ///     A perfectly ordinary admission — the baseline everything else varies from.
        /// </summary>
        public static Admission AnOrdinaryAdmission() {
            return new Admission(new Patient("943 476 5919", "A. Okonkwo"), "Ward 7", new DateOnly(2026, 7, 30), "Standard");
        }

        /// <summary>
        ///     One that has been waiting long enough for discharge planning to be overdue.
        /// </summary>
        public static Admission AnAdmissionAwaitingDischargeFor(int days) {
            return AnOrdinaryAdmission() with { ArrivedOn = new DateOnly(2026, 8, 5).AddDays(-days) };
        }

        /// <summary>
        ///     One the triage nurse marked for immediate attention.
        /// </summary>
        public static Admission AnUrgentAdmission() {
            return AnOrdinaryAdmission() with { TriageCategory = "Immediate" };
        }

        #endregion

    }

}
