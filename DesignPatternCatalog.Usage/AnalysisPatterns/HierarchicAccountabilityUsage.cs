#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.HierarchicAccountabilitySample {

    // A fire and rescue service at a large incident. Sixty firefighters, nine appliances, four sectors and an
    // incident commander, and the one thing that has to be true of the whole arrangement is that every officer
    // on the ground knows the single officer they report to.
    //
    // A general ACCOUNTABILITY does not promise that. It is deliberately permissive: two commissioners for one
    // party is a legitimate shape, and for "budget holder" alongside "safeguarding lead" it is the shape you
    // want. Command is the case where it is not — an officer with two commanders receives two orders, and the
    // model that allowed it produced them.
    //
    // The constraint belongs on the KIND, not on the relationship, and that is the whole point of where the
    // annotation goes. "A party may be responsible to only one accountability of this type" is a sentence
    // about *every* accountability of that kind, present and future, including the ones nobody has created
    // yet. Written on an instance it would be a fact about that instance; written on the type it is a rule the
    // type can enforce on its own instances, and the only place from which it can be enforced before the
    // instance exists.
    //
    // So the role is held by the type object — the class that subtypes the accountability type — and the
    // entry is catalogued as a specialisation of ACCOUNTABILITY, so that a rule written for accountabilities
    // in general still reaches these. They *are* accountabilities. What they add is a shape.
    //
    // What the shape buys is that `CommanderOf` can return one officer rather than a collection. Every chart,
    // every escalation path and every "who do I tell" question downstream is written against that
    // single-valuedness, and none of them would survive its being false.

    /// <summary>
    ///     A kind of accountability constrained so that its instances form a hierarchy.
    /// </summary>
    /// <remarks>
    ///     The role sits here rather than on <see cref="CommandLine" /> because the constraint is a statement
    ///     about the kind. Enforcement lives here too, for the same reason: it has to happen before an instance
    ///     exists.
    /// </remarks>
    [HierarchicAccountability]
    public sealed class IncidentCommandType {

        private readonly List<CommandLine> _lines = new();

        public IncidentCommandType(string name) {
            Name = name;
        }

        /// <summary>What the service calls it: incident command, sector command.</summary>
        public string Name { get; }

        /// <summary>The command lines of this kind currently in force.</summary>
        public IReadOnlyList<CommandLine> Lines => _lines;

        /// <summary>
        ///     Establishes a command line of this kind, refusing what the constraint forbids.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     If the subordinate already answers to an officer under this kind, or if the line would close a
        ///     cycle.
        /// </exception>
        public CommandLine Establish(FireOfficer commander, FireOfficer responsible) {
            if (CommanderOf(responsible) is not null) {
                throw new InvalidOperationException($"{responsible.CallSign} already reports to an officer under {Name}");
            }

            if (ReferenceEquals(commander, responsible) || IsAbove(responsible, commander)) {
                throw new InvalidOperationException("a command line may not close a cycle");
            }

            CommandLine line = new(this, commander, responsible);
            _lines.Add(line);

            return line;
        }

        /// <summary>
        ///     The one officer this one reports to, or none if they are the incident commander. Single-valued
        ///     only because of the constraint above, and everything downstream assumes it.
        /// </summary>
        public FireOfficer? CommanderOf(FireOfficer officer) {
            foreach (CommandLine line in _lines) {
                if (ReferenceEquals(line.Responsible, officer)) {
                    return line.Commander;
                }
            }

            return null;
        }

        /// <summary>Whether one officer is anywhere above another in the chain.</summary>
        public bool IsAbove(FireOfficer candidate, FireOfficer officer) {
            FireOfficer? above = CommanderOf(officer);
            while (above is not null) {
                if (ReferenceEquals(above, candidate)) {
                    return true;
                }

                above = CommanderOf(above);
            }

            return false;
        }

    }

    /// <summary>
    ///     One command relationship: this officer reports to that one, under a stated kind.
    /// </summary>
    /// <remarks>
    ///     Carries no constraint of its own. It cannot: what is being asserted is true of the kind, and an
    ///     instance can only be checked once it exists.
    /// </remarks>
    public sealed class CommandLine {

        internal CommandLine(IncidentCommandType type, FireOfficer commander, FireOfficer responsible) {
            Type        = type;
            Commander   = commander;
            Responsible = responsible;
        }

        /// <summary>The kind of command this is.</summary>
        public IncidentCommandType Type { get; }

        /// <summary>The officer commanding — the commissioner of the responsibility.</summary>
        public FireOfficer Commander { get; }

        /// <summary>The officer commanded.</summary>
        public FireOfficer Responsible { get; }

    }

    /// <summary>
    ///     An officer at the incident.
    /// </summary>
    [Party]
    public sealed class FireOfficer {

        public FireOfficer(string callSign, string rank) {
            CallSign = callSign;
            Rank     = rank;
        }

        /// <summary>How they are addressed over the radio.</summary>
        public string CallSign { get; }

        /// <summary>Their rank, which does not by itself establish who they report to.</summary>
        public string Rank { get; }

    }

}
