#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.UnitOfWorkSample {

    // Everything a business transaction touched, written out in one go.

    [UnitOfWork]
    public interface IOrderingUnitOfWork : IDisposable {

        void Commit();
        void Rollback();

    }

    [UnitOfWork]
    public sealed class SqlOrderingUnitOfWork : IOrderingUnitOfWork {

        private readonly List<object> _pending = new();

        public void Track(object entity) => _pending.Add(entity);

        public void Commit()   => _pending.Clear();
        public void Rollback() => _pending.Clear();

        public void Dispose() => _pending.Clear();

    }

}
