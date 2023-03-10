namespace PyriteSharp.source.Syntax.Parser.Expressions;

public class BlockStatement : Statement
{
    public Token LeftBracket;
    public List<Statement> Expression;
    public Token RightBracket;
    public BlockStatement(Token left, List<Statement> e, Token right)
    {
        LeftBracket = left;
        Expression = e;
        RightBracket = right;
    }

    public override TokenType Type => TokenType.STATEMENT;
    public override IEnumerable<Node> GetChildren()
    {
        yield return LeftBracket;
        foreach (var e in Expression)
        {
            yield return e;
        }
        yield return RightBracket;
    }
}