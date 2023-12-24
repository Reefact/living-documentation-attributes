#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Attribute to mark a method as the Factory Method in the Factory Method design pattern.
    /// </summary>
    /// <remarks>
    ///     This serves as a form of documentation and can also be used for static analysis.
    /// </remarks>
    /// <example>
    ///     Here is a simple example of using the FactoryMethodAttribute:
    ///     <code>
    /// public class ConcreteCreator : Creator
    /// {
    ///     [FactoryMethod]
    ///     public override Product FactoryMethod()
    ///     {
    ///         return new ConcreteProduct();
    ///     }
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class FactoryMethodAttribute : LivingDocumentationAttribute {

        // Implementation details

    }

}