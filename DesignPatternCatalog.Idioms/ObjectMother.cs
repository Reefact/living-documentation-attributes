#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.Idioms {

    /// <summary>
    ///     ObjectMother (no catalog of its own) — A class that builds fully formed objects for tests, so that a test
    ///     states what matters about its data and nothing else.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Peter Schuh and Stephanie Punke, <i>ObjectMother: Easing Test Object Creation in XP (XP Universe)</i>,
    ///         2001.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class ObjectMotherAttribute : DesignPatternAttribute { }

}
