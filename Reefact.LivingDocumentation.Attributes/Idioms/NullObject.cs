#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Idioms {

    /// <summary>
    ///     NullObject (no catalog of its own) — A special case whose behaviour is to do nothing, so that the absence of
    ///     a collaborator needs no null check.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         A narrower case of SpecialCase, in Patterns of Enterprise Application Architecture: every participant
    ///         annotated here is one of those too, and a consumer asking for the broader pattern gets these as well.
    ///     </para>
    ///     <para>
    ///         Bobby Woolf, <i>Pattern Languages of Program Design 3</i>, 1997.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public sealed class NullObjectAttribute : EnterpriseApplicationArchitecture.SpecialCaseAttribute { }

}
