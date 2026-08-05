#region Usings declarations

using Reefact.LivingDocumentation.Attributes.GangOfFour;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.GangOfFour.ProxySample {

    // A report that is expensive to build, and is therefore built only if it is read.

    [Proxy.Subject]
    public interface IAnnualReport {

        string Content { get; }

    }

    [Proxy.RealSubject(Subject = typeof(IAnnualReport))]
    public sealed class AnnualReport : IAnnualReport {

        public AnnualReport() { Content = "…"; }

        public string Content { get; }

    }

    [Proxy.Proxy(Subject = typeof(IAnnualReport), RealSubject = typeof(AnnualReport))]
    public sealed class LazyAnnualReport : IAnnualReport {

        private AnnualReport? _real;

        public string Content => (_real ??= new AnnualReport()).Content;

    }

}
