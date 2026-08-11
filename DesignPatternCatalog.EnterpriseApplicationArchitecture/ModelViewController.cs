#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseApplicationArchitecture {

    /// <summary>
    ///     ModelViewController (Patterns of Enterprise Application Architecture) — Splits presentation into three: what
    ///     is true, how it is shown, and what a request does about it.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Patterns of Enterprise Application Architecture</i>, 2002.
    ///     </para>
    /// </remarks>
    public static class ModelViewController {

        /// <summary>
        ///     Role played by a type or a member in the ModelViewController design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     What the presentation is about, and the only one of the three that knows nothing of the other two. A
        ///     model that references its view has lost the separation the pattern exists for — it is the direction of
        ///     that ignorance, not the number of classes, that makes this the pattern.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ModelAttribute : Role { }

        /// <summary>
        ///     One rendering of the model. Several may show one model at once, which is why the model may not know
        ///     them: the day a second view appears, anything the model knew about the first is wrong.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ViewAttribute : Role {

            /// <summary>
            ///     The <see cref="ModelAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Model { get; init; }

        }

        /// <summary>
        ///     What a request does. It interprets the input, asks the model to change, and chooses the view — and holds
        ///     no domain rule of its own, because a rule that lives here cannot be reached by anything that is not a
        ///     request.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ControllerAttribute : Role {

            /// <summary>
            ///     The <see cref="ModelAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Model { get; init; }

            /// <summary>
            ///     The <see cref="ViewAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? View { get; init; }

        }

    }

}
