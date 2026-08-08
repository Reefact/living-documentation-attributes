#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.AnalysisPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AnalysisPatterns.PostSample {

    // A ferry operator running eleven vessels in the Hebrides. The master of a vessel is answerable, in law,
    // for its safe operation — and the master of MV Kerrera changes every six weeks, because that is how a
    // rotation works.
    //
    // The model that hangs the responsibility on the person is wrong in a way that is invisible on the day it
    // is written and expensive later. Every responsibility attached to a master has to be moved on handover:
    // the safety-management accountability, the drill schedule, the defect authority, the standing order to
    // report to the harbour authority. Miss one and the vessel has, on paper, nobody answerable for it —
    // which is exactly the state an audit is looking for and nothing in the system reports.
    //
    // POST is simpler than the machinery one reaches for. A post is not a new kind of thing with a holder
    // hanging off it: it is a THIRD KIND OF PARTY, alongside person and organization. "Master, MV Kerrera" is
    // a party. It can be made responsible for something exactly as a person can, because everything that
    // attaches to a party attaches to it, and nothing new had to be built for that to be true.
    //
    // That is what the annotation says, and it is why the entry is catalogued as a specialisation of PARTY: a
    // rule written to find parties finds posts, which is the property the whole arrangement rests on.
    //
    // The appointment is deliberately NOT part of the pattern. Who occupies the post is a separate fact with
    // its own dates, and keeping it separate is what makes a handover one insertion rather than a migration of
    // every responsibility. The vacancy falls out of the same separation: a post between holders is not an
    // error and is not nothing — the vessel does not sail, and something has to be able to ask.

    /// <summary>
    ///     Whoever the operator can hold answerable: a person, a company, or a post.
    /// </summary>
    /// <remarks>
    ///     Fowler's PARTY. Responsibilities are stated in terms of this and nothing narrower, which is what
    ///     lets a post hold one.
    /// </remarks>
    [Party]
    public abstract class Answerable {

        protected Answerable(string name) {
            Name = name;
        }

        /// <summary>What it is called.</summary>
        public string Name { get; }

    }

    /// <summary>
    ///     A position that exists whether or not anyone occupies it — and a party in its own right.
    /// </summary>
    /// <remarks>
    ///     It adds no mechanism for attaching responsibilities. It does not need one: it is a party, so the
    ///     ordinary way of making a party answerable already reaches it.
    /// </remarks>
    [Post]
    public sealed class ShipboardPost : Answerable {

        public ShipboardPost(string title, string vessel) : base($"{title}, {vessel}") {
            Title  = title;
            Vessel = vessel;
        }

        /// <summary>Master, chief engineer, chief officer.</summary>
        public string Title { get; }

        /// <summary>The vessel the post belongs to.</summary>
        public string Vessel { get; }

    }

    /// <summary>
    ///     A person who may be appointed to a post.
    /// </summary>
    /// <remarks>
    ///     Holds no responsibilities of the vessel. That is not an omission: a responsibility recorded here is
    ///     one the handover does not move.
    /// </remarks>
    public sealed class Officer : Answerable {

        public Officer(string name, string certificateNumber) : base(name) {
            CertificateNumber = certificateNumber;
        }

        /// <summary>The certificate of competency they sail on, which belongs to them and not to the post.</summary>
        public string CertificateNumber { get; }

    }

    /// <summary>
    ///     Who occupies a post, and from when. Separate from the post on purpose.
    /// </summary>
    /// <remarks>
    ///     Not a participant in the pattern — the pattern is that the post is a party. This is what the
    ///     separation buys: a handover is one of these, and no responsibility is touched.
    /// </remarks>
    public sealed class Appointment {

        public Appointment(ShipboardPost post, Officer officer, DateOnly from, DateOnly? until) {
            Post    = post;
            Officer = officer;
            From    = from;
            Until   = until;
        }

        /// <summary>The post appointed to.</summary>
        public ShipboardPost Post { get; }

        /// <summary>The officer appointed.</summary>
        public Officer Officer { get; }

        /// <summary>When the appointment began.</summary>
        public DateOnly From { get; }

        /// <summary>When it ended, if it has.</summary>
        public DateOnly? Until { get; }

        /// <summary>Whether this appointment was in force on a given day.</summary>
        public bool InForceOn(DateOnly day) {
            return day >= From && (Until is null || day <= Until);
        }

    }

    /// <summary>
    ///     The appointments on record, and the two questions the separation makes answerable.
    /// </summary>
    public sealed class CrewingRegister {

        private readonly List<Appointment> _appointments = new();

        /// <summary>Records an appointment.</summary>
        public void Appoint(Appointment appointment) {
            _appointments.Add(appointment);
        }

        /// <summary>Who held a post on a given day — the question an audit asks about the past.</summary>
        public Officer? HolderOn(ShipboardPost post, DateOnly day) {
            foreach (Appointment appointment in _appointments) {
                if (ReferenceEquals(appointment.Post, post) && appointment.InForceOn(day)) {
                    return appointment.Officer;
                }
            }

            return null;
        }

        /// <summary>
        ///     Whether a post is unfilled on a given day, which is a state and not an absence of one.
        /// </summary>
        public bool IsVacantOn(ShipboardPost post, DateOnly day) {
            return HolderOn(post, day) is null;
        }

    }

}
