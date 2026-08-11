#region Usings declarations

using DesignPatternCatalog.GangOfFour;

#endregion

namespace DesignPatternCatalog.Usage.GangOfFour.StateSample {

    // A support ticket whose behaviour changes with its status.

    [State.State]
    public interface ITicketState {

        ITicketState Escalate();
        bool         AcceptsComments { get; }

    }

    [State.ConcreteState(State = typeof(ITicketState))]
    public sealed class Open : ITicketState {

        public ITicketState Escalate()        => new Escalated();
        public bool         AcceptsComments   => true;

    }

    [State.ConcreteState(State = typeof(ITicketState))]
    public sealed class Escalated : ITicketState {

        public ITicketState Escalate()      => this;
        public bool         AcceptsComments => true;

    }

    [State.ConcreteState(State = typeof(ITicketState))]
    public sealed class Closed : ITicketState {

        public ITicketState Escalate()      => new Open();
        public bool         AcceptsComments => false;

    }

    [State.Context(State = typeof(ITicketState))]
    public sealed class Ticket {

        private ITicketState _state = new Open();

        public bool AcceptsComments => _state.AcceptsComments;

        public void Escalate() => _state = _state.Escalate();

    }

}
