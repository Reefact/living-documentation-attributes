#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.AnalysisPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AnalysisPatterns.OrganizationStructureSample {

    // A supermarket group: four hundred stores, nine distribution centres, and two organisation charts that do
    // not agree.
    //
    // The legal one follows ownership — the group owns a holding company, which owns the chain that owns the
    // store, and a franchise store is owned by somebody else entirely. The trading one follows how the business
    // is run: the store reports to an area, the area to a region, the region to a trading director. A store
    // sits in both, under different parents, and neither chart is a distortion of the other. Both are asked
    // real questions: one by the auditors, one by the people who set next week's promotions.
    //
    // A model with a parent reference answers one of them. A model with two parent references answers two, and
    // has to be edited when the group adds a third — which it does, the first time it reports by sustainability
    // footprint.
    //
    // ORGANIZATION STRUCTURE makes the relationship the object and gives it a type. A new chart is a row.
    //
    // It is catalogued as a specialisation of ACCOUNTABILITY, and the reason is visible rather than theoretical:
    // Fowler draws figure 2.6 exactly as he draws figure 2.8, with *organization* where *party* stands and
    // *parent* / *subsidiary* where *commissioner* / *responsible* stand. So a rule written to find
    // accountabilities finds these, which is right — restricting both ends to organizations is what this adds,
    // not a different thing being said.
    //
    // The dates are not decoration. A reorganisation does not erase what preceded it, and a question about last
    // quarter has to be asked against the structure that was in force then. A model that overwrites a parent
    // reference has answered it wrongly and cannot know.

    /// <summary>
    ///     What kind of structure this is, and which organizations may stand at each end.
    /// </summary>
    /// <remarks>
    ///     A new chart is configured here. It also holds the admissibility rule, so "may this store sit under
    ///     that area" is asked in one place rather than in each screen that moves one.
    /// </remarks>
    [OrganizationStructure.OrganizationStructureType]
    public sealed class StructureKind {

        public StructureKind(string name, bool crossesLegalOwnership) {
            Name                  = name;
            CrossesLegalOwnership = crossesLegalOwnership;
        }

        /// <summary>Legal ownership, trading line, sustainability reporting.</summary>
        public string Name { get; }

        /// <summary>
        ///     Whether this kind may link organizations that do not own one another — true of a trading line, and
        ///     the reason a franchise store can report to an area without being owned by it.
        /// </summary>
        public bool CrossesLegalOwnership { get; }

    }

    /// <summary>
    ///     One relationship between two organizations, of one kind, for a period.
    /// </summary>
    [OrganizationStructure.OrganizationStructure(OrganizationStructureType = typeof(StructureKind))]
    public sealed class GroupStructure {

        public GroupStructure(StructureKind kind, GroupOrganization parent, GroupOrganization subsidiary,
                              DateOnly from, DateOnly? until) {
            Kind       = kind;
            Parent     = parent;
            Subsidiary = subsidiary;
            From       = from;
            Until      = until;
        }

        /// <summary>Which chart this link belongs to.</summary>
        public StructureKind Kind { get; }

        /// <summary>The organization above.</summary>
        [OrganizationStructure.Parent]
        public GroupOrganization Parent { get; }

        /// <summary>The organization below.</summary>
        [OrganizationStructure.Subsidiary]
        public GroupOrganization Subsidiary { get; }

        /// <summary>When the link took effect.</summary>
        public DateOnly From { get; }

        /// <summary>When it ended, if it has.</summary>
        public DateOnly? Until { get; }

        /// <summary>Whether this link was in force on a given day.</summary>
        public bool InForceOn(DateOnly day) {
            return day >= From && (Until is null || day <= Until);
        }

    }

    /// <summary>
    ///     A store, an area, a region, a holding company.
    /// </summary>
    [Party]
    public sealed class GroupOrganization {

        public GroupOrganization(string name) {
            Name = name;
        }

        /// <summary>Its name.</summary>
        public string Name { get; }

    }

    /// <summary>
    ///     The links on record, and the question two charts make answerable.
    /// </summary>
    public sealed class GroupModel {

        private readonly List<GroupStructure> _links = new();

        /// <summary>Records a link.</summary>
        public void Add(GroupStructure link) {
            _links.Add(link);
        }

        /// <summary>
        ///     The parent of an organization within one chart, on a given day. One chart, one answer — which is
        ///     what a single parent reference could not give once there were two charts.
        /// </summary>
        public GroupOrganization? ParentOf(GroupOrganization organization, StructureKind kind, DateOnly day) {
            foreach (GroupStructure link in _links) {
                if (ReferenceEquals(link.Subsidiary, organization)
                 && ReferenceEquals(link.Kind, kind)
                 && link.InForceOn(day)) {
                    return link.Parent;
                }
            }

            return null;
        }

    }

}
