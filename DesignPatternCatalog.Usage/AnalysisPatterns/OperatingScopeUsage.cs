#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.OperatingScopeSample {

    // A customs brokerage. A broker is authorised to clear goods on a client's behalf — but never simply
    // "authorised". The authorisation always reads: at these ports, for these commodity codes, up to this
    // declared value.
    //
    // Those clauses are the thing the business argues about, and they are not known when the authorisation is
    // written. A broker is added to Felixstowe in March, has textiles removed in June after an error, and has
    // their ceiling raised in September — while the authorisation itself continues, unchanged, throughout.
    //
    // That is why they are not fields. A model with `Ports`, `CommodityCodes` and `ValueCeiling` on the
    // authorisation has to be edited each time the business invents a clause, and it has nowhere to put the
    // one nobody predicted.
    //
    // OPERATING SCOPES are those clauses, hung on the accountability the way line items hang on an order. The
    // authorisation says *that* the broker may act; the scopes say *what for*.
    //
    // The kinds are SUBTYPES, and that is the part worth getting right. Each subtype names what it is a scope
    // of — a port, a commodity code, a declared value — so two clauses are told apart by their type rather
    // than by a string field nothing can range over. It also means each kind carries the data its own question
    // needs: a ceiling has an amount and a port does not, and neither has to pretend otherwise.
    //
    // What the annotation buys is the multiplicity nobody writes down: a scope belongs to exactly one
    // authorisation. A scope shared between two of them is not a shortcut, it is a clause that changes on one
    // client's authorisation when someone edits another's — and it compiles.

    /// <summary>
    ///     One clause of what an authorisation covers.
    /// </summary>
    /// <remarks>
    ///     Belongs to exactly one authorisation. The kinds below are subtypes rather than a type field,
    ///     because each answers a different question and carries different data.
    /// </remarks>
    [OperatingScope]
    public abstract class AuthorisationClause {

        protected AuthorisationClause(ClearanceAuthorisation authorisation) {
            Authorisation = authorisation;
        }

        /// <summary>The one authorisation this clause qualifies.</summary>
        public ClearanceAuthorisation Authorisation { get; }

        /// <summary>Whether this clause admits what a declaration states for the aspect it governs.</summary>
        public abstract bool Admits(CustomsDeclaration declaration);

    }

    /// <summary>
    ///     A port the broker may clear at.
    /// </summary>
    public sealed class PortScope : AuthorisationClause {

        public PortScope(ClearanceAuthorisation authorisation, string portCode) : base(authorisation) {
            PortCode = portCode;
        }

        /// <summary>The port, as a UN/LOCODE.</summary>
        public string PortCode { get; }

        /// <inheritdoc />
        public override bool Admits(CustomsDeclaration declaration) {
            return declaration.PortCode == PortCode;
        }

    }

    /// <summary>
    ///     A commodity heading the broker may clear.
    /// </summary>
    public sealed class CommodityScope : AuthorisationClause {

        public CommodityScope(ClearanceAuthorisation authorisation, string heading) : base(authorisation) {
            Heading = heading;
        }

        /// <summary>The tariff heading, matched on its prefix so a heading covers its subheadings.</summary>
        public string Heading { get; }

        /// <inheritdoc />
        public override bool Admits(CustomsDeclaration declaration) {
            return declaration.CommodityCode.StartsWith(Heading, System.StringComparison.Ordinal);
        }

    }

    /// <summary>
    ///     The most the broker may declare on one entry.
    /// </summary>
    /// <remarks>
    ///     Carries an amount, which no other kind of clause here does — the reason the kinds are subtypes.
    /// </remarks>
    public sealed class ValueCeiling : AuthorisationClause {

        public ValueCeiling(ClearanceAuthorisation authorisation, decimal maximum) : base(authorisation) {
            Maximum = maximum;
        }

        /// <summary>The ceiling, in the declaration's currency.</summary>
        public decimal Maximum { get; }

        /// <inheritdoc />
        public override bool Admits(CustomsDeclaration declaration) {
            return declaration.DeclaredValue <= Maximum;
        }

    }

    /// <summary>
    ///     What a broker proposes to clear.
    /// </summary>
    public sealed class CustomsDeclaration {

        public CustomsDeclaration(string portCode, string commodityCode, decimal declaredValue) {
            PortCode      = portCode;
            CommodityCode = commodityCode;
            DeclaredValue = declaredValue;
        }

        /// <summary>Where it is being cleared.</summary>
        public string PortCode { get; }

        /// <summary>What is being cleared.</summary>
        public string CommodityCode { get; }

        /// <summary>For how much.</summary>
        public decimal DeclaredValue { get; }

    }

    /// <summary>
    ///     A broker's authorisation to act for a client, with the clauses that say what for.
    /// </summary>
    /// <remarks>
    ///     Clauses are grouped by kind before being asked: alternatives within a kind, conjunction across
    ///     kinds. Two ports are alternatives; a ceiling is not an alternative to anything.
    /// </remarks>
    public sealed class ClearanceAuthorisation {

        private readonly List<AuthorisationClause> _clauses = new();

        public ClearanceAuthorisation(string broker, string client) {
            Broker = broker;
            Client = client;
        }

        /// <summary>The broker authorised.</summary>
        public string Broker { get; }

        /// <summary>The client they act for.</summary>
        public string Client { get; }

        /// <summary>Every clause in force.</summary>
        public IReadOnlyList<AuthorisationClause> Clauses => _clauses;

        /// <summary>Adds a clause. The authorisation itself is untouched.</summary>
        public void Add(AuthorisationClause clause) {
            _clauses.Add(clause);
        }

        /// <summary>
        ///     Whether a declaration falls within scope: for every kind of clause present, at least one clause
        ///     of that kind must admit it.
        /// </summary>
        public bool Covers(CustomsDeclaration declaration) {
            Dictionary<string, bool> satisfiedByKind = new();
            foreach (AuthorisationClause clause in _clauses) {
                string kind = clause.GetType().Name;
                satisfiedByKind[kind] = satisfiedByKind.TryGetValue(kind, out bool already) && already
                                     || clause.Admits(declaration);
            }

            foreach (KeyValuePair<string, bool> kind in satisfiedByKind) {
                if (!kind.Value) {
                    return false;
                }
            }

            return true;
        }

    }

}
