#region Usings declarations

using DesignPatternCatalog.GangOfFour;

#endregion

namespace DesignPatternCatalog.Usage.GangOfFour.VisitorSample {

    // New operations over an expression tree, without reopening the node types.

    [Visitor.Visitor]
    public interface IExpressionVisitor {

        [Visitor.VisitMethod(ConcreteElement = typeof(Literal))]
        void Visit(Literal literal);

        [Visitor.VisitMethod(ConcreteElement = typeof(Sum))]
        void Visit(Sum sum);

    }

    [Visitor.Element]
    public interface IExpression {

        [Visitor.AcceptMethod]
        void Accept(IExpressionVisitor visitor);

    }

    [Visitor.ConcreteElement(Element = typeof(IExpression))]
    public sealed record Literal(decimal Value) : IExpression {

        public void Accept(IExpressionVisitor visitor) => visitor.Visit(this);

    }

    [Visitor.ConcreteElement(Element = typeof(IExpression))]
    public sealed record Sum(IExpression Left, IExpression Right) : IExpression {

        public void Accept(IExpressionVisitor visitor) => visitor.Visit(this);

    }

    [Visitor.ObjectStructure(Element = typeof(IExpression))]
    public sealed class ExpressionTree {

        public ExpressionTree(IExpression root) { Root = root; }

        public IExpression Root { get; }

        public void Walk(IExpressionVisitor visitor) => Root.Accept(visitor);

    }

    [Visitor.ConcreteVisitor(Visitor = typeof(IExpressionVisitor))]
    public sealed class Evaluator : IExpressionVisitor {

        private decimal _result;

        public decimal Result => _result;

        public void Visit(Literal literal) => _result = literal.Value;

        public void Visit(Sum sum) {
            sum.Left.Accept(this);
            decimal left = _result;
            sum.Right.Accept(this);
            _result += left;
        }

    }

}
