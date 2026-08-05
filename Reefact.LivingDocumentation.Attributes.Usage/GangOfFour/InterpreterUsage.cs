#region Usings declarations

using Reefact.LivingDocumentation.Attributes.GangOfFour;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.GangOfFour.InterpreterSample {

    // A tiny boolean language used to describe eligibility rules.

    [Interpreter.Context]
    public sealed class Facts {

        private readonly HashSet<string> _true = new();

        public void Assert(string fact) => _true.Add(fact);
        public bool Holds(string fact)  => _true.Contains(fact);

    }

    [Interpreter.AbstractExpression]
    public interface IRule {

        bool Evaluate(Facts facts);

    }

    [Interpreter.TerminalExpression(AbstractExpression = typeof(IRule))]
    public sealed class Fact : IRule {

        private readonly string _name;

        public Fact(string name) { _name = name; }

        public bool Evaluate(Facts facts) => facts.Holds(_name);

    }

    [Interpreter.NonterminalExpression(AbstractExpression = typeof(IRule))]
    public sealed class And : IRule {

        private readonly IRule _left;
        private readonly IRule _right;

        public And(IRule left, IRule right) {
            _left  = left;
            _right = right;
        }

        public bool Evaluate(Facts facts) => _left.Evaluate(facts) && _right.Evaluate(facts);

    }

}
