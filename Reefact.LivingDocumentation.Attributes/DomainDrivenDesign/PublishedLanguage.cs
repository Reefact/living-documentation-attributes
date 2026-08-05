#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     PublishedLanguage (Domain-Driven Design) — A well-documented shared language, used as the medium of
    ///     translation between contexts rather than as anyone's internal model.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Eric Evans, <i>Domain-Driven Design</i>, 2003.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public sealed class PublishedLanguageAttribute : LivingDocumentationAttribute { }

}
