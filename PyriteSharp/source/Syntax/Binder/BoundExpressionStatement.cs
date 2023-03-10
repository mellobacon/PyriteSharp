using PyriteSharp.source.Syntax.Parser.Expressions;

namespace PyriteSharp.source.Syntax.Binder;

public class BoundExpressionStatement : BoundStatement
{
    public BoundExpression Expression;
    public BoundExpressionStatement(BoundExpression expression)
    {
        Expression = expression;
    }

    public override BoundType BoundType => BoundType.EXPRESSION_STATEMENT;
}