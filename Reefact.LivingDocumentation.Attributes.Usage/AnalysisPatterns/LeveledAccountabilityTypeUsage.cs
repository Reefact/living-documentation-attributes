#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.AnalysisPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AnalysisPatterns.LeveledAccountabilityTypeSample {

    // A bank's credit approval ladder. A relationship manager may commit the bank to four hundred thousand; a
    // branch head to two million; regional credit to twenty; beyond that it is the credit committee. Every
    // facility is approved by someone, and every approver answers for it to the level above.
    //
    // What matters, and what an ordinary accountability cannot say, is that the ladder MAY NOT BE SKIPPED. A
    // relationship manager whose facility is signed off directly by the credit committee has not been given
    // extra assurance: the branch head who would have seen the concentration in one postcode never saw it. That
    // is the failure the ladder exists to prevent, and it looks like diligence while it happens.
    //
    // LEVELED ACCOUNTABILITY TYPE is the constraint stated as an ordered list of party types: a party may be
    // responsible only to a party whose type is the next one along. It is the sharper of the two ways of
    // constraining direction — naming which types may stand at each end forbids reversal, but a list of levels
    // forbids skipping as well.
    //
    // The role is on the type object, for the reason its siblings share: a list of levels is a statement about
    // every accountability of the kind, including the ones nobody has created, and enforcement has to be able
    // to happen before an instance exists.
    //
    // Fowler draws it in figure 2.12 as an «overlapping» subtype of accountability type, beside the hierarchic
    // one. The two axes are independent: this constrains *which* level answers to which, the hierarchic one
    // constrains *how many* commissioners a party may have.

    /// <summary>
    ///     A kind of accountability whose rule is an ordered list of party types.
    /// </summary>
    /// <remarks>
    ///     Holds the levels and enforces them, because a list of levels describes the kind rather than any one
    ///     of its instances.
    /// </remarks>
    [LeveledAccountabilityType]
    public sealed class ApprovalLadder {

        private readonly List<string> _levels;

        public ApprovalLadder(string name, IEnumerable<string> levelsLowestFirst) {
            Name    = name;
            _levels = new List<string>(levelsLowestFirst);
        }

        /// <summary>What the credit policy calls it.</summary>
        public string Name { get; }

        /// <summary>The party types in order, lowest first.</summary>
        public IReadOnlyList<string> Levels => _levels;

        /// <summary>
        ///     Records that one approver answers to another, refusing a step that reverses or skips a level.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     If the two types are not adjacent in the ladder, in that order.
        /// </exception>
        public CreditApproval Approve(CreditOfficer responsible, CreditOfficer commissioner, decimal amount) {
            int below = _levels.IndexOf(responsible.Grade);
            int above = _levels.IndexOf(commissioner.Grade);

            if (below < 0 || above < 0) {
                throw new InvalidOperationException($"a grade outside the {Name} ladder cannot approve within it");
            }

            if (above != below + 1) {
                throw new InvalidOperationException(
                    $"{responsible.Grade} answers to {_levels[below + 1]} under {Name}, not to {commissioner.Grade}");
            }

            return new CreditApproval(this, responsible, commissioner, amount);
        }

    }

    /// <summary>
    ///     One approval, of one kind, between two adjacent levels.
    /// </summary>
    public sealed class CreditApproval {

        internal CreditApproval(ApprovalLadder ladder, CreditOfficer responsible, CreditOfficer commissioner,
                                decimal amount) {
            Ladder       = ladder;
            Responsible  = responsible;
            Commissioner = commissioner;
            Amount       = amount;
        }

        /// <summary>The ladder this approval was taken under.</summary>
        public ApprovalLadder Ladder { get; }

        /// <summary>The officer who committed the bank.</summary>
        public CreditOfficer Responsible { get; }

        /// <summary>The officer they answer to for it.</summary>
        public CreditOfficer Commissioner { get; }

        /// <summary>What was committed.</summary>
        public decimal Amount { get; }

    }

    /// <summary>
    ///     Someone who may approve credit, at a stated grade.
    /// </summary>
    [Party]
    public sealed class CreditOfficer {

        public CreditOfficer(string name, string grade) {
            Name  = name;
            Grade = grade;
        }

        /// <summary>Their name.</summary>
        public string Name { get; }

        /// <summary>Their grade, which is the party type the ladder is stated in terms of.</summary>
        public string Grade { get; }

    }

}
