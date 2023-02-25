namespace PyriteSharp.source.Syntax.Parser.Expressions;

public class BinaryExpression : Expression
{
    public Expression left;
    public Token op;
    public Expression right;

    public BinaryExpression(Expression left, Token op, Expression right)
    {
        this.left = left;
        this.op = op;
        this.right = right;
    }
    public override TokenType Type => TokenType.BINARY_EXPRESSION;
    public override IEnumerable<Node> GetChildren()
    {
        yield return left;
        yield return op;
        yield return right;
    }
}