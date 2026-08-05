#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.TableDataGatewaySample {

    // Regional library: every statement that touches the `loan` table, in one place.
    //
    // A TABLE DATA GATEWAY is a gateway narrowed to one table, and its two defining traits are easy to lose:
    //
    //   * it is STATELESS — it holds no row, only the statements;
    //   * it handles the WHOLE table — one instance serves every loan there is.
    //
    // Both show in the signatures below: the methods take and return plain data, and the type has no fields
    // at all. Give it a `_currentLoan` and it has quietly become a row data gateway; give it a
    // `RenewIfMemberHasNoFines()` and it has become an active record with extra steps.
    //
    // It suits exactly this: a schema whose rows are handled in bulk by code that does not want objects.
    // The overdue sweep runs nightly across forty thousand loans and cares about none of them individually.

    /// <summary>
    ///     All the SQL for the `loan` table, and nothing else.
    /// </summary>
    [TableDataGateway]
    public interface ILoanTableGateway {

        LoanRow? FindByBarcode(string barcode);

        IReadOnlyCollection<LoanRow> FindOverdueOn(DateOnly date);

        void Insert(LoanRow row);

        void UpdateReturnedOn(long loanId, DateOnly returnedOn);

        void Delete(long loanId);

    }

    /// <summary>
    ///     A row of the table, carried as data — no behaviour, no identity, no persistence of its own.
    /// </summary>
    public sealed record LoanRow(long LoanId, string Barcode, long MemberId, DateOnly DueOn, DateOnly? ReturnedOn);

}
