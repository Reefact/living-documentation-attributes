#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.ValueObjectSample {

    // Hotel booking: the nights a reservation covers.
    //
    // This sample exists to show the *narrower* reading of value object, and it is the one place in
    // the catalog where the difference between two patterns of the same name is visible in code
    // rather than argued in prose.
    //
    // Fowler's value object asks one thing: that equality not be based on identity. Two date ranges
    // covering the same nights are the same range — there is no "which one", and a booking system
    // that gave each one an identifier would be inventing a distinction nobody in the hotel makes.
    // That is the whole of the requirement, and `DateRange` below meets it.
    //
    // It is deliberately mutable. A reservations clerk extends a stay by a night, and the range the
    // booking holds is amended in place. That is ordinary, and Fowler's pattern permits it: nothing
    // in "equality is not identity" says the value cannot change.
    //
    // It is also exactly why this is NOT Evans' value object. Compare
    // DomainDrivenDesign/ValueObjectUsage.cs: there, immutability is the point, because the value is
    // a statement about the domain that a later edit would falsify. Run Evans' rule over the type
    // below and it fails; run Fowler's over it and it passes. Two patterns, one name, and the
    // assertions tell them apart — which is why the catalog keeps both and relates them by
    // inheritance rather than merging them (ADR-0007).
    //
    // The consequence of mutability is the copy in `Amend`: a value object that can change must not
    // be shared, or amending one booking silently amends another that happened to hold the same
    // instance.

    [ValueObject]
    public sealed class DateRange {

        public DateRange(DateOnly arrival, DateOnly departure) {
            if (departure <= arrival) { throw new ArgumentException("A stay is at least one night.", nameof(departure)); }

            Arrival   = arrival;
            Departure = departure;
        }

        public DateOnly Arrival   { get; private set; }
        public DateOnly Departure { get; private set; }

        public int Nights => Departure.DayNumber - Arrival.DayNumber;

        // Mutating, and legitimate under this reading of the pattern.
        public void Amend(DateOnly departure) {
            if (departure <= Arrival) { throw new ArgumentException("A stay is at least one night.", nameof(departure)); }

            Departure = departure;
        }

        // What the pattern does require: equality on the value, never on the instance.
        public override bool Equals(object? obj) {
            return obj is DateRange other && other.Arrival == Arrival && other.Departure == Departure;
        }

        public override int GetHashCode() => HashCode.Combine(Arrival, Departure);

        public override string ToString() => $"{Arrival:yyyy-MM-dd} → {Departure:yyyy-MM-dd} ({Nights} nights)";

    }

    public sealed class Reservation {

        public Reservation(string reference, DateRange stay) {
            Reference = reference;
            // Copied in, not shared: the caller keeps a range it may amend, and a booking must not
            // change because someone else's range did.
            Stay = new DateRange(stay.Arrival, stay.Departure);
        }

        public string    Reference { get; }
        public DateRange Stay      { get; }

    }

}
