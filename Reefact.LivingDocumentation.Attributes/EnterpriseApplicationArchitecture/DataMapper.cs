#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture {

    /// <summary>
    ///     DataMapper (Patterns of Enterprise Application Architecture) — A layer that moves data between objects and a
    ///     database while keeping them independent of each other, and of it.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         A narrower case of Mapper, in Patterns of Enterprise Application Architecture: every participant
    ///         annotated here is one of those too, and a consumer asking for the broader pattern gets these as well.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Patterns of Enterprise Application Architecture</i>, 2002.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class DataMapperAttribute : EnterpriseApplicationArchitecture.MapperAttribute { }

}
