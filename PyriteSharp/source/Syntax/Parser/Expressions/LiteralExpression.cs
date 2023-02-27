namespace PyriteSharp.source.Syntax.Parser.Expressions;

public class LiteralExpression : Expression
{
    private readonly Token _token;
    public readonly object? Value;
    public LiteralExpression(Token token, object? value)
    {
        _token = token;
        Value = value;
    }

    public override TokenType Type => TokenType.LITERAL_EXPRESSION;
    public override IEnumerable<Node> GetChildren()
    {
        yield return _token;
    }
}