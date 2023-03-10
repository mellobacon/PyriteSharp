namespace PyriteSharp.source.Syntax.Parser.Expressions;

public class ExpressionStatement : Statement
{
    public Expression Expression;
    public ExpressionStatement(Expression e)
    {
        Expression = e;
    }

    public override TokenType Type => TokenType.EXPRESSION_STATEMENT;
    public override IEnumerable<Node> GetChildren()
    {
        yield return Expression;
    }
}