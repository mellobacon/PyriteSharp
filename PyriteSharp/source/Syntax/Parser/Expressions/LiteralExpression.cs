namespace PyriteSharp.source.Syntax.Parser.Expressions;

public class LiteralExpression : Expression
{
    private readonly Token _token;
    public object? value;
    public LiteralExpression(Token token, object? value)
    {
        _token = token;
        this.value = value;
    }

    public override TokenType Type => TokenType.LITERAL_EXPRESSION;
    public override IEnumerable<Node> GetChildren()
    {
        yield return _token;
    }
}