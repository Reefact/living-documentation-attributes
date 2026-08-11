#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.ActiveRecordSample {

    // Regional library: the reservation queue, where the model really is the table.
    //
    // An ACTIVE RECORD carries a row, the access to that row, AND the domain logic that acts on it. Beside
    // the row data gateway next door, exactly one thing has been added — behaviour — and that one thing is
    // the pattern.
    //
    // It is chosen here rather than tolerated. A reservation is a row: a member, a title, a position in the
    // queue, a date it was placed. The rules are about that row and nothing else — you may cancel your own
    // reservation, a reservation expires seven days after the book arrives — and there is no aggregate to
    // speak of. Splitting this across a domain object, a mapper and a repository would produce three files
    // that say what one says.
    //
    // The trade is stated honestly in the pattern and worth stating here: this holds only while the model
    // follows the schema. The day reservations gain a rule that spans several tables — a member's total
    // reservation allowance across branches — this class starts reaching for other tables, and the pattern
    // is being outgrown rather than misapplied. That is when a DATA MAPPER earns its cost.

    /// <summary>
    ///     A place in the queue for a title, with the rules that govern it and the ability to persist itself.
    /// </summary>
    [ActiveRecord]
    public sealed class Reservation {

        public long     ReservationId { get; private set; }
        public long     MemberId      { get; private set; }
        public string   Isbn          { get; private set; } = "";
        public DateOnly PlacedOn      { get; private set; }
        public DateOnly? AvailableOn  { get; private set; }

        #region Statics members declarations

        public static Reservation Find(long reservationId) {
            return new Reservation();
        }

        #endregion

        /// <summary>
        ///     Seven days from the day the copy arrived, after which the next member in the queue gets it.
        /// </summary>
        public bool HasExpiredOn(DateOnly date) {
            return AvailableOn is { } available && date > available.AddDays(7);
        }

        public void CancelBy(long memberId) {
            if (memberId != MemberId) { throw new InvalidOperationException("A reservation is cancelled by the member who placed it."); }

            Delete();
        }

        public void Save() { }

        public void Delete() { }

    }

}
