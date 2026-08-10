#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.SetupDecoratorSample {

    // The customs tests need a stub customs service listening on a port. Which tests those are is decided at
    // run time by a category filter, so no single class can own the arrangement.
    //
    // SETUP DECORATOR wraps the suite instead.

    /// <summary>
    ///     Wraps a suite: arranges before it runs, tears down after.
    /// </summary>
    /// <remarks>
    ///     The arrangement travels with the suite rather than with a class, which is what lets it apply to a
    ///     set chosen at run time. It is also the piece a reader will not find by looking in the test —
    ///     because it is not there.
    /// </remarks>
    [SetupDecorator]
    public sealed class WithStubCustomsService {

        private readonly object _suite;

        public WithStubCustomsService(object suite) {
            _suite = suite;
        }

        public void Run() { }

    }
}
