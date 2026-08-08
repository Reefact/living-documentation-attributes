#region Usings declarations

using Reefact.LivingDocumentation.Attributes.AnalysisPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AnalysisPatterns.PartySample {

    // A regional water utility. Eighty thousand connections: houses, a few hundred farms, two paper mills and
    // a caravan park that is somebody's sole trade. Every one of them has an address to send a bill to, a
    // phone number to ring when a main bursts, and a balance.
    //
    // The model that gets written first has a Household and a Company, and both of them grow an address, a
    // billing preference and a contact number. Nothing is wrong with it on the day it is written. What goes
    // wrong is the second year: a rule about paperless billing gets added to one of them, the meter-reading
    // appointment letter is taught to look up the other, and a year later nobody can answer "how do we
    // contact this customer" without asking which of the two they are.
    //
    // PARTY is the supertype both of them turn out to have wanted. Everything that is true of *whoever the
    // utility deals with* attaches here, once.
    //
    // The role is deliberately on the supertype alone. Annotating the subtypes would say that being a person
    // or being an organization is the interesting fact, and it is the opposite: the pattern's whole claim is
    // that at the point where an address hangs, the difference does not matter. What does not attach here is
    // just as telling — a farm has a holding number and a household does not, so that stays below.
    //
    // The assertion this licenses is worth stating because nothing else in the code makes it. A field typed
    // `Household` where `Subscriber` was meant compiles, passes its tests, and quietly excludes the paper
    // mills from whatever it does. A rule that can find the party can find that.

    /// <summary>
    ///     Whoever the utility supplies, bills and has to be able to reach.
    /// </summary>
    /// <remarks>
    ///     Address and contact hang here rather than on the two subtypes, which is the whole of the pattern:
    ///     one place to look, one place to change, and no possibility of the two drifting apart.
    /// </remarks>
    [Party]
    public abstract class Subscriber {

        protected Subscriber(string reference, string billingAddress, string contactNumber) {
            Reference      = reference;
            BillingAddress = billingAddress;
            ContactNumber  = contactNumber;
        }

        /// <summary>The account reference printed on every bill.</summary>
        public string Reference { get; }

        /// <summary>Where bills go.</summary>
        public string BillingAddress { get; }

        /// <summary>Who is rung when a main bursts.</summary>
        public string ContactNumber { get; }

    }

    /// <summary>
    ///     A person supplied in their own name.
    /// </summary>
    /// <remarks>
    ///     Holds no address and no contact number: it would be a second copy of the ones above.
    /// </remarks>
    public sealed class Householder : Subscriber {

        public Householder(string reference, string billingAddress, string contactNumber, string surname)
            : base(reference, billingAddress, contactNumber) {
            Surname = surname;
        }

        /// <summary>The name the account is in.</summary>
        public string Surname { get; }

    }

    /// <summary>
    ///     A business, a farm or a trust supplied as an entity.
    /// </summary>
    /// <remarks>
    ///     What it adds below the supertype is what is genuinely not true of a person: a trade-effluent
    ///     consent number. That is the test of whether something belongs here rather than above.
    /// </remarks>
    public sealed class SuppliedOrganization : Subscriber {

        public SuppliedOrganization(string reference, string billingAddress, string contactNumber,
                                    string registeredName, string? effluentConsent)
            : base(reference, billingAddress, contactNumber) {
            RegisteredName  = registeredName;
            EffluentConsent = effluentConsent;
        }

        /// <summary>The name on the register.</summary>
        public string RegisteredName { get; }

        /// <summary>The consent under which it may discharge to the sewer, where it has one.</summary>
        public string? EffluentConsent { get; }

    }

}
