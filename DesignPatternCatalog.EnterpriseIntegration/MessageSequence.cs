#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     MessageSequence (Enterprise Integration Patterns) — Marks each message of a set with its place and the set's
    ///     extent, so that an arbitrarily large body of data can travel as many messages and be reassembled.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Gregor Hohpe, Bobby Woolf, <i>Enterprise Integration Patterns</i>, 2003.
    ///     </para>
    /// </remarks>
    public static class MessageSequence {

        /// <summary>
        ///     Role played by a type or a member in the MessageSequence design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     The property naming the set a message belongs to. Without it two large transfers interleaved on one
        ///     channel cannot be told apart, which is the failure the pattern is written against.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class SequenceIdentifierAttribute : Role { }

        /// <summary>
        ///     The property giving the message's place in the set. It is what lets a receiver reassemble in order
        ///     however the messages arrive, and what a resequencer works from.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class PositionAttribute : Role { }

        /// <summary>
        ///     The property saying how many there are, or marking the last one. It is what lets a receiver know the set
        ///     is complete rather than merely quiet — the same question an aggregator's completeness condition asks.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class SizeAttribute : Role { }

    }

}
