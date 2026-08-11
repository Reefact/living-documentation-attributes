#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.Posa2 {

    /// <summary>
    ///     StrategizedLocking (Pattern-Oriented Software Architecture, Volume 2) — Strategizes a component's
    ///     synchronization by making its lock a pluggable type, so one implementation serves every concurrency use-
    ///     case.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Douglas Schmidt, Michael Stal, Hans Rohnert, Frank Buschmann, <i>Pattern-Oriented Software Architecture,
    ///         Volume 2</i>, 2000.
    ///     </para>
    /// </remarks>
    public static class StrategizedLocking {

        /// <summary>
        ///     Role played by a type or a member in the StrategizedLocking design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The component whose synchronization is configured into it rather than written into it. There is one
        ///     implementation of the component and that is the point: a fix applied here reaches every configuration,
        ///     where a family of near-identical classes drifts apart.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
        public sealed class ComponentAttribute : Role { }

        /// <summary>
        ///     One member of the family of locking strategies a component can be configured with — a mutex, a readers-
        ///     writer lock, or the null lock whose acquire and release do nothing and cost nothing in a single-threaded
        ///     configuration. Every member answers the same acquire and release, which is the whole of what makes them
        ///     interchangeable.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class LockingStrategyAttribute : Role { }

    }

}
