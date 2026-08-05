#region Usings declarations

using Reefact.LivingDocumentation.Attributes.GangOfFour;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.GangOfFour.AbstractFactorySample {

    // Rendering a report: each output format is a coherent family of parts.

    [AbstractFactory.AbstractFactory]
    public interface IReportFactory {

        IReportHeader CreateHeader(string title);
        IReportBody   CreateBody();

    }

    [AbstractFactory.AbstractProduct]
    public interface IReportHeader { }

    [AbstractFactory.AbstractProduct]
    public interface IReportBody { }

    [AbstractFactory.ConcreteFactory(AbstractFactory = typeof(IReportFactory))]
    public sealed class PdfReportFactory : IReportFactory {

        public IReportHeader CreateHeader(string title) => new PdfHeader(title);
        public IReportBody   CreateBody()               => new PdfBody();

    }

    [AbstractFactory.ConcreteProduct(AbstractProduct = typeof(IReportHeader))]
    public sealed class PdfHeader : IReportHeader {

        public PdfHeader(string title) { Title = title; }

        public string Title { get; }

    }

    [AbstractFactory.ConcreteProduct(AbstractProduct = typeof(IReportBody))]
    public sealed class PdfBody : IReportBody { }

}
