namespace PyriteSharp.source.Syntax.Parser.Expressions;

public class BinaryExpression : Expression
{
    public readonly Expression Left;
    public readonly Token Op;
    public readonly Expression Right;

    public BinaryExpression(Expression left, Token op, Expression right)
    {
        Left = left;
        Op = op;
        Right = right;
    }
    public override TokenType Type => TokenType.BINARY_EXPRESSION;
    public override IEnumerable<Node> GetChildren()
    {
        yield return Left;
        yield return Op;
        yield return Right;
    }
}