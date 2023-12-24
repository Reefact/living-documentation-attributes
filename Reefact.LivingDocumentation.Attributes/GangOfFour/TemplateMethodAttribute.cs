#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark methods as the template method in the Template Method design pattern.
    /// </summary>
    /// <remarks>
    ///     Use this attribute to indicate that a method serves as the template method in the Template Method pattern.
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class TemplateMethodAttribute : LivingDocumentationAttribute { }

}