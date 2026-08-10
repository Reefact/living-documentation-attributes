#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.MicroservicesPatterns {

    /// <summary>
    ///     AntiCorruptionLayer (Microservices Patterns) — Translates between a legacy monolith's domain model and a new
    ///     service's, so that the model being escaped does not reach the model being built.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         Chris Richardson, <i>Microservices Patterns</i>, 2018.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public sealed class AntiCorruptionLayerAttribute : LivingDocumentationAttribute { }

}
