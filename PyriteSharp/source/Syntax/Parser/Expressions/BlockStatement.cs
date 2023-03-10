namespace PyriteSharp.source.Syntax.Parser.Expressions;

public class BlockStatement : Statement
{
    private readonly Token _leftBracket;
    public readonly List<Statement> Expression;
    private readonly Token _rightBracket;
    public BlockStatement(Token left, List<Statement> e, Token right)
    {
        _leftBracket = left;
        Expression = e;
        _rightBracket = right;
    }

    public override TokenType Type => TokenType.STATEMENT;
    public override IEnumerable<Node> GetChildren()
    {
        yield return _leftBracket;
        foreach (var e in Expression)
        {
            yield return e;
        }
        yield return _rightBracket;
    }
}