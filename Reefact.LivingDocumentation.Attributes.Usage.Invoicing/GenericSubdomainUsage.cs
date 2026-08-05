#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DomainDrivenDesign;

#endregion

// Regional rail: charging operators for the track they used.
//
// This assembly bills train operating companies for access to the network. It has invoices, credit notes,
// tax rules, payment terms, dunning — and none of it is why anyone would choose this operator over another.
// Every railway in Europe bills track access, and they all bill it the same way, because the way is fixed
// by regulation and by accountancy rather than by anything the business decided.
//
// So it is a GENERIC SUBDOMAIN. The word to notice is not "unimportant" — an unbilled month is a very
// serious problem — but "undistinctive". The test Evans gives is whether it could be bought, outsourced or
// replaced by a published solution without weakening what the organisation is actually good at. Here the
// honest answer is yes: this could be an off-the-shelf billing package tomorrow, and the railway would run
// exactly as well.
//
// Saying so in the code is what the annotation is for, and it is a statement about where effort should NOT
// go. The subtle modelling, the design reviews and the best people belong in Train Operations, which is
// annotated as the core domain. A team that spends a quarter perfecting a dunning workflow while path
// allocation stays crude has made a mistake this annotation exists to make visible.
//
// It is still a bounded context of its own: an "operator" here is a legal counterparty with a billing
// address and a VAT number, which is not what "operator" means next door.

[assembly: GenericSubdomain]

namespace Reefact.LivingDocumentation.Attributes.Usage.Invoicing.GenericSubdomainSample {

    /// <summary>
    ///     What an operator owes for one month of track access.
    /// </summary>
    public sealed class TrackAccessInvoice {

        public TrackAccessInvoice(string operatorVatNumber, DateOnly period, decimal amountExcludingTax) {
            OperatorVatNumber  = operatorVatNumber;
            Period             = period;
            AmountExcludingTax = amountExcludingTax;
        }

        public string   OperatorVatNumber  { get; }
        public DateOnly Period             { get; }
        public decimal  AmountExcludingTax { get; }

    }

}
