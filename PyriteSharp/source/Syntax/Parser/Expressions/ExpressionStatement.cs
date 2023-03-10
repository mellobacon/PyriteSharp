namespace PyriteSharp.source.Syntax.Parser.Expressions;

public class ExpressionStatement : Statement
{
    public readonly Expression Statement;
    public ExpressionStatement(Expression e)
    {
        Statement = e;
    }

    public override TokenType Type => TokenType.EXPRESSION_STATEMENT;
    public override IEnumerable<Node> GetChildren()
    {
        yield return Statement;
    }
}