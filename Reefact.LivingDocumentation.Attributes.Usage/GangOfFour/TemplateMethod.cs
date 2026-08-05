#region Usings declarations

using Reefact.LivingDocumentation.Attributes.GangOfFour;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.GangOfFour.TemplateMethodSample {

    // The import sequence is fixed; parsing and validation are supplied by the subclass.

    [TemplateMethod.AbstractClass]
    public abstract class ImportJob {

        [TemplateMethod.TemplateMethod]
        public void Import(string payload) {
            IReadOnlyList<string> rows = Parse(payload);
            foreach (string row in rows) { Store(row); }
            OnCompleted(rows.Count);
        }

        [TemplateMethod.PrimitiveOperation]
        protected abstract IReadOnlyList<string> Parse(string payload);

        [TemplateMethod.PrimitiveOperation]
        protected abstract void Store(string row);

        [TemplateMethod.HookOperation]
        protected virtual void OnCompleted(int rowCount) { }

    }

    [TemplateMethod.ConcreteClass(AbstractClass = typeof(ImportJob))]
    public sealed class CsvImportJob : ImportJob {

        protected override IReadOnlyList<string> Parse(string payload) => payload.Split('\n');

        protected override void Store(string row) { }

    }

}
