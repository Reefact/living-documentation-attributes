#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DependencyInjection;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.DependencyInjection.ControlFreakSample {

    // The station's composition root was introduced last quarter, and eleven classes were left behind
    // because they construct what they use and nothing outside them can say otherwise. Nobody wrote them
    // that way on purpose; they were written before anybody asked the question.
    //
    // Annotating them is not a confession, it is a count. The build now knows there are eleven, and the
    // rule is "no more than eleven, and never more" — which is the only architecture rule that works on
    // code that already exists. Without the annotation you cannot write it, because you cannot tell the
    // eleven you have accepted from the twelfth somebody adds next Tuesday.
    //
    // That is what this annotation is for. It is not detection: a control freak that annotates itself is
    // an honest one, and the one worth catching is the one nobody marked.

    /// <summary>
    ///     Plays the station's jingles between programmes.
    /// </summary>
    /// <remarks>
    ///     One of the eleven. It constructs its own reader, so the choice of where jingles come from is
    ///     sealed inside it: the relay stations cannot point it at their own library, and a test cannot
    ///     point it at a fixture. That second consequence is how it was noticed — there is no unit test
    ///     for this class, and there cannot be one without a disk.
    ///     <para>
    ///         The migration is a constructor parameter and a line in the composition root. It has not
    ///         been done because the class works, which is the correct reason to leave it and the wrong
    ///         reason to forget it.
    ///     </para>
    /// </remarks>
    [ControlFreak]
    public sealed class JinglePlayer {

        private readonly JingleLibraryReader _reader;

        public JinglePlayer(string libraryPath) {
            // The dependency is chosen here, by this class, and by nobody else.
            _reader = new JingleLibraryReader(libraryPath);
        }

        public string? NextJingle(string forProgramme) {
            return _reader.Read(forProgramme);
        }

    }

    public sealed class JingleLibraryReader {

        private readonly string _libraryPath;

        public JingleLibraryReader(string libraryPath) {
            _libraryPath = libraryPath;
        }

        public string? Read(string forProgramme) {
            return _libraryPath.Length == 0 ? null : $"{forProgramme}-ident";
        }

    }

}
