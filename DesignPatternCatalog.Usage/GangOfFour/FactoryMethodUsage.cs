#region Usings declarations

using DesignPatternCatalog.GangOfFour;

#endregion

namespace DesignPatternCatalog.Usage.GangOfFour.FactoryMethodSample {

    // The export pipeline is fixed; which writer it runs on is decided by the subclass.

    [FactoryMethod.Product]
    public interface IExportWriter {

        void Write(string row);

    }

    [FactoryMethod.ConcreteProduct(Product = typeof(IExportWriter))]
    public sealed class CsvWriter : IExportWriter {

        public void Write(string row) { }

    }

    [FactoryMethod.Creator]
    public abstract class ExportJob {

        public void Run(IEnumerable<string> rows) {
            IExportWriter writer = CreateWriter();
            foreach (string row in rows) { writer.Write(row); }
        }

        [FactoryMethod.FactoryMethod]
        protected abstract IExportWriter CreateWriter();

    }

    [FactoryMethod.ConcreteCreator(Creator = typeof(ExportJob), ConcreteProduct = typeof(CsvWriter))]
    public sealed class CsvExportJob : ExportJob {

        protected override IExportWriter CreateWriter() => new CsvWriter();

    }

}
