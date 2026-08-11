#region Usings declarations

using DesignPatternCatalog.GangOfFour;

#endregion

namespace DesignPatternCatalog.Usage.GangOfFour.MediatorSample {

    // Form widgets that stay unaware of each other.

    [Mediator.Mediator]
    public interface IFormCoordinator {

        void Changed(FormWidget source);

    }

    [Mediator.Colleague(Mediator = typeof(IFormCoordinator))]
    public abstract class FormWidget {

        protected FormWidget(IFormCoordinator coordinator) { Coordinator = coordinator; }

        protected IFormCoordinator Coordinator { get; }

        public bool IsEnabled { get; set; } = true;

    }

    [Mediator.ConcreteColleague(Colleague = typeof(FormWidget))]
    public sealed class CountryPicker : FormWidget {

        private string _country = string.Empty;

        public CountryPicker(IFormCoordinator coordinator) : base(coordinator) { }

        public string Country {
            get => _country;
            set {
                _country = value;
                Coordinator.Changed(this);
            }
        }

    }

    [Mediator.ConcreteColleague(Colleague = typeof(FormWidget))]
    public sealed class StatePicker : FormWidget {

        public StatePicker(IFormCoordinator coordinator) : base(coordinator) { }

    }

    [Mediator.ConcreteMediator(Mediator = typeof(IFormCoordinator))]
    public sealed class AddressForm : IFormCoordinator {

        public CountryPicker Country { get; }
        public StatePicker   State   { get; }

        public AddressForm() {
            Country = new CountryPicker(this);
            State   = new StatePicker(this);
        }

        public void Changed(FormWidget source) {
            if (ReferenceEquals(source, Country)) { State.IsEnabled = Country.Country == "US"; }
        }

    }

}
