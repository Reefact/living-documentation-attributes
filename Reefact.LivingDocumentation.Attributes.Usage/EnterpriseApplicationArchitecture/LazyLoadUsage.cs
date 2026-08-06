#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.LazyLoadSample {

    // A genealogical archive: loading one person without loading the eighteenth century.
    //
    // A person has parents, who have parents. Loading a person eagerly loads their ancestry, and their
    // ancestry's ancestry — which for a well-documented family is forty thousand records to render one
    // name on one page.
    //
    // A LAZY LOAD holds enough to be useful and fetches the rest when something asks. `Parents` below is
    // not populated until it is read, and a page that only shows a name never touches it.
    //
    // The annotation is here mainly because of what this pattern COSTS when it is invisible, and that is
    // worth being blunt about. Render a descendant list of two hundred people, touch `Parents` on each,
    // and this loads two hundred times — one query per person, none of them at a call site that looks like
    // it is querying anything. That is the ripple effect, it is the classic performance failure of the
    // pattern, and nothing in `person.Parents` warns anyone.
    //
    // Annotating it does not fix that. What it does is make the property findable: a reviewer looking at a
    // loop can ask whether anything in it is lazy, and an architecture rule can list every lazy load a page
    // touches. The pattern is a trade, and the annotation is what stops the trade being made silently.

    /// <summary>
    ///     A person in the archive, holding their own facts and fetching their ancestry only if asked.
    /// </summary>
    [LazyLoad]
    public sealed class Person {

        private readonly Func<long, IReadOnlyCollection<Person>> _fetchParents;
        private IReadOnlyCollection<Person>?                     _parents;

        public Person(long id, string name, Func<long, IReadOnlyCollection<Person>> fetchParents) {
            Id            = id;
            Name          = name;
            _fetchParents = fetchParents;
        }

        public long   Id   { get; }
        public string Name { get; }

        /// <summary>
        ///     The parents — one query, on first read. In a loop, one query per person.
        /// </summary>
        public IReadOnlyCollection<Person> Parents => _parents ??= _fetchParents(Id);

    }

}
