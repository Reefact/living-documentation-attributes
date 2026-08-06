#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DomainDrivenDesign;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.DomainDrivenDesign.SmartUiSample {

    // A pop-up vaccination clinic in a leisure centre, open for eleven days. One receptionist, one laptop,
    // a spreadsheet of appointment slots, and a rule so short it fits in a sentence: nobody is booked into a
    // slot that is already full.
    //
    // Evans names the SMART UI as the anti-pattern, and then does something the name does not prepare you for
    // — he gives the circumstances under which it is the right answer, and this is them. The application
    // exists for eleven days. There is no second channel and there will not be one. The rule will not change,
    // because the clinic will be gone before anyone could change it. A layered architecture here would cost
    // more than the clinic.
    //
    // So the rule lives in the screen, on purpose, and the annotation says "on purpose". That is the whole of
    // what it adds, and it is not nothing: without it, the next person to read this file sees a screen with
    // business logic in it and starts extracting a service — which is the correct instinct applied to the one
    // case where it is wrong.
    //
    // What the annotation is really doing is fixing a SCOPE. Every rule about layering stops at this class,
    // and it stops here because someone decided it, with a reason a reviewer can argue with.
    //
    // And it names its own expiry. The moment a second channel appears — a booking site, a phone line, an
    // import from the regional register — the reason evaporates, because the rule below would then hold for
    // one caller out of three. That is the day the annotation has to come off first, and having it there is
    // what makes it a decision to revisit rather than a habit nobody remembers taking.

    /// <summary>
    ///     The booking screen, rules included, for eleven days.
    /// </summary>
    /// <remarks>
    ///     Annotated rather than refactored. Extracting a model here would produce a domain layer, an
    ///     application service and a repository for one rule, in a system that closes before any of them could
    ///     pay for themselves.
    /// </remarks>
    [SmartUi]
    public sealed class AppointmentSheet {

        private const int PlacesPerSlot = 12;

        private readonly Dictionary<string, List<string>> _booked = new(StringComparer.Ordinal);

        /// <summary>
        ///     What the button does, and where the only rule in the system lives.
        /// </summary>
        public string Book(string slot, string patient) {
            if (!_booked.TryGetValue(slot, out List<string>? names)) {
                names         = new List<string>();
                _booked[slot] = names;
            }

            if (names.Count >= PlacesPerSlot) { return $"{slot} is full — try the next one."; }
            if (names.Contains(patient, StringComparer.OrdinalIgnoreCase)) { return $"{patient} is already booked into {slot}."; }

            names.Add(patient);

            return $"{patient} booked into {slot} ({names.Count} of {PlacesPerSlot}).";
        }

        public IReadOnlyList<string> Slot(string slot) {
            return _booked.TryGetValue(slot, out List<string>? names) ? names : Array.Empty<string>();
        }

    }

}
