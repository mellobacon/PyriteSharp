namespace PyriteSharp.source.Syntax.Parser.Expressions;

public class GroupedExpression : Expression
{
    private Token _leftparen;
    public readonly Expression Expression;
    private Token _rightparen;
    public GroupedExpression(Token leftparen, Expression expression, Token rightparen)
    {
        _leftparen = leftparen;
        Expression = expression;
        _rightparen = rightparen;
    }
    public override TokenType Type => TokenType.GROUPED_EXPRESSION;
    public override IEnumerable<Node> GetChildren()
    {
        yield return _leftparen;
        yield return Expression;
        yield return _rightparen;
    }
}