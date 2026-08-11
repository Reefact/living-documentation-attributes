#region Usings declarations

using DesignPatternCatalog.GangOfFour;

#endregion

namespace DesignPatternCatalog.Usage.GangOfFour.PrototypeSample {

    // Document templates cloned rather than rebuilt from scratch.

    [Prototype.Prototype]
    public interface IDocumentTemplate {

        [Prototype.CloneMethod]
        IDocumentTemplate Duplicate();

    }

    [Prototype.ConcretePrototype(Prototype = typeof(IDocumentTemplate))]
    public sealed class ContractTemplate : IDocumentTemplate {

        private readonly List<string> _clauses;

        public ContractTemplate(IEnumerable<string> clauses) { _clauses = clauses.ToList(); }

        public IDocumentTemplate Duplicate() => new ContractTemplate(_clauses);

    }

}
