#region Usings declarations

using System.Text;

using DesignPatternCatalog.GangOfFour;

#endregion

namespace DesignPatternCatalog.Usage.GangOfFour.BuilderSample {

    // The same invoice layout, rendered into different representations.

    [Builder.Product]
    public sealed class Invoice {

        public Invoice(string content) { Content = content; }

        public string Content { get; }

    }

    [Builder.Builder]
    public interface IInvoiceBuilder {

        void AddHeader(string customer);
        void AddLine(string label, decimal amount);

    }

    [Builder.ConcreteBuilder(Builder = typeof(IInvoiceBuilder), Product = typeof(Invoice))]
    public sealed class TextInvoiceBuilder : IInvoiceBuilder {

        private readonly StringBuilder _content = new();

        public void AddHeader(string customer)             => _content.AppendLine($"Invoice for {customer}");
        public void AddLine(string label, decimal amount)  => _content.AppendLine($"  {label}: {amount:N2}");

        public Invoice Build() => new(_content.ToString());

    }

    [Builder.Director(Builder = typeof(IInvoiceBuilder))]
    public sealed class InvoiceWriter {

        public void Write(IInvoiceBuilder builder, string customer) {
            builder.AddHeader(customer);
            builder.AddLine("Subscription", 49.90m);
        }

    }

}
